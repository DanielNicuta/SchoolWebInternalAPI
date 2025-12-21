using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolWebInternalAPI.Application.Interfaces.Auth;
using SchoolWebInternalAPI.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolWebInternalAPI.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            if (!await _userManager.CheckPasswordAsync(user, password))
                return null;

            return GenerateToken(user);
        }

        public async Task<bool> RegisterAsync(string email, string password, string fullName)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FullName = fullName
            };

            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }


        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("fullName", user.FullName ?? "")
            };
            if (string.IsNullOrWhiteSpace(_jwt.Key))
                throw new InvalidOperationException("JWT Key is not configured. Check Jwt:Key in appsettings.json.");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
