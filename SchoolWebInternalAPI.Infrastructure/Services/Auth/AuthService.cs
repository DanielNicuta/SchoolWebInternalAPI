using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolWebInternalAPI.Application.DTOs.Auth;
using SchoolWebInternalAPI.Application.Interfaces.Auth;
using SchoolWebInternalAPI.Domain.Entities.Auth;
using SchoolWebInternalAPI.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolWebInternalAPI.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SchoolDbContext _db;
        private readonly JwtSettings _jwt;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SchoolDbContext db,
            IOptions<JwtSettings> jwt)
        {
            _userManager = userManager;
            _db = db;
            _jwt = jwt.Value;
        }

        public async Task<AuthResponseDto?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            if (await _userManager.IsLockedOutAsync(user))
                return null;

            var ok = await _userManager.CheckPasswordAsync(user, password);

            if (!ok)
            {
                await _userManager.AccessFailedAsync(user);
                return null;
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var accessToken = await GenerateAccessToken(user);

            // NEW: token family per login/session
            var familyId = Guid.NewGuid();
            var refreshToken = await CreateRefreshTokenAsync(user.Id, familyId, parentTokenId: null);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
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

        public async Task<AuthResponseDto?> RefreshAsync(string refreshToken)
        {
            // Track changes because we will revoke/rotate
            var tokenEntity = await _db.RefreshTokens
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (tokenEntity == null)
                return null;

            // FAMILY REUSE DETECTION:
            // If token is revoked and has been replaced, someone is trying to reuse it
            if (!tokenEntity.IsActive)
            {
                if (tokenEntity.RevokedAt != null && !string.IsNullOrWhiteSpace(tokenEntity.ReplacedByToken))
                {
                    // Revoke entire family on reuse attempt
                    await RevokeFamilyAsync(tokenEntity.UserId, tokenEntity.FamilyId);
                }

                return null;
            }

            var user = await _userManager.FindByIdAsync(tokenEntity.UserId);
            if (user == null) return null;

            // ROTATION: revoke old
            tokenEntity.RevokedAt = DateTime.UtcNow;

            var newRefresh = GenerateRefreshToken();
            tokenEntity.ReplacedByToken = newRefresh;

            // Create new refresh token row in the SAME FAMILY
            var newEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefresh,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDays <= 0 ? 7 : _jwt.RefreshTokenDays),

                // NEW: keep chain/family
                FamilyId = tokenEntity.FamilyId,
                ParentTokenId = tokenEntity.Id
            };

            _db.RefreshTokens.Add(newEntity);
            await _db.SaveChangesAsync();

            var accessToken = await GenerateAccessToken(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefresh
            };
        }

        public async Task<bool> RevokeAsync(string refreshToken)
        {
            var tokenEntity = await _db.RefreshTokens
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (tokenEntity == null || tokenEntity.RevokedAt != null)
                return false;

            tokenEntity.RevokedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RevokeAllAsync(string userId)
        {
            var now = DateTime.UtcNow;

            var tokens = await _db.RefreshTokens
                .Where(x => x.UserId == userId && x.RevokedAt == null && x.ExpiresAt > now)
                .ToListAsync();

            if (tokens.Count == 0)
                return false;

            foreach (var t in tokens)
                t.RevokedAt = now;

            await _db.SaveChangesAsync();
            return true;
        }

        // NEW: revoke all tokens in a family
        private async Task RevokeFamilyAsync(string userId, Guid familyId)
        {
            var now = DateTime.UtcNow;

            var tokens = await _db.RefreshTokens
                .Where(x => x.UserId == userId && x.FamilyId == familyId && x.RevokedAt == null)
                .ToListAsync();

            if (tokens.Count == 0)
                return;

            foreach (var t in tokens)
                t.RevokedAt = now;

            await _db.SaveChangesAsync();
        }

        // --------------------
        // helpers
        // --------------------

        private async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            if (string.IsNullOrWhiteSpace(_jwt.Key))
                throw new InvalidOperationException("JWT Key is not configured. Check Jwt:Key in appsettings.json.");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new("fullName", user.FullName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Roles for [Authorize(Roles="Admin")]
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

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

        // UPDATED: add family + parent
        private async Task<string> CreateRefreshTokenAsync(string userId, Guid familyId, Guid? parentTokenId)
        {
            var refresh = GenerateRefreshToken();

            var entity = new RefreshToken
            {
                UserId = userId,
                Token = refresh,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDays <= 0 ? 7 : _jwt.RefreshTokenDays),

                FamilyId = familyId,
                ParentTokenId = parentTokenId
            };

            _db.RefreshTokens.Add(entity);
            await _db.SaveChangesAsync();

            return refresh;
        }

        private static string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }
    }
}
