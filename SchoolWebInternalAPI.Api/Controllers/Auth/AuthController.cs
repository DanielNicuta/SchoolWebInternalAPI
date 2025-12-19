using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.Interfaces.Auth;

namespace SchoolWebInternalAPI.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var token = await _auth.LoginAsync(request.Email, request.Password);
            if (token == null) return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var success = await _auth.RegisterAsync(request.Email, request.Password, request.FullName);
            if (!success) return BadRequest("Could not create user.");

            return Ok("User created");
        }
    }

    public record LoginDto(string Email, string Password);
    public record RegisterDto(string Email, string Password, string FullName);
}
