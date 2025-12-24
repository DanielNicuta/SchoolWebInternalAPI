using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Auth;
using SchoolWebInternalAPI.Application.Interfaces.Auth;

namespace SchoolWebInternalAPI.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        [EnableRateLimiting("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto request)
        {
            var result = await _auth.LoginAsync(request.Email, request.Password);

            if (result == null)
                return Unauthorized(ApiResponse<AuthResponseDto>.Fail("Invalid credentials.", 401));

            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful."));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<bool>>> Register([FromBody] RegisterDto request)
        {
            var ok = await _auth.RegisterAsync(request.Email, request.Password, request.FullName ?? String.Empty);

            if (!ok)
                return BadRequest(ApiResponse<bool>.Fail("Registration failed.", 400));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Registration successful."));
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Refresh([FromBody] RevokeRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest(ApiResponse<AuthResponseDto>.Fail("Refresh token is required.", 400));

            var result = await _auth.RefreshAsync(request.RefreshToken);

            if (result == null)
                return Unauthorized(ApiResponse<AuthResponseDto>.Fail("Invalid or expired refresh token.", 401));

            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Token refreshed."));
        }

        [HttpPost("revoke")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<bool>>> Revoke([FromBody] RevokeRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest(ApiResponse<bool>.Fail("Refresh token is required.", 400));

            var ok = await _auth.RevokeAsync(request.RefreshToken);

            if (!ok)
                return NotFound(ApiResponse<bool>.NotFound("Refresh token not found or already revoked."));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Refresh token revoked."));
        }
        // ----------------------------------
        // LOGOUT
        // ----------------------------------
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto)
        {
            var revoked = await _auth.RevokeAsync(dto.RefreshToken);

            if (!revoked)
                return BadRequest("Invalid or already revoked refresh token.");

            return Ok("Logged out successfully.");
        }
        [Authorize] // any logged user
        [HttpPost("logout-all")]
        public async Task<IActionResult> LogoutAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? User.FindFirstValue("sub"); // fallback

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var revoked = await _auth.RevokeAllAsync(userId);

            // 200 even if none found is acceptable; your choice:
            return Ok(new { success = true, revokedAny = revoked });
        }
    }
}
