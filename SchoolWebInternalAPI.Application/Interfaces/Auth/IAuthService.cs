using SchoolWebInternalAPI.Application.DTOs.Auth;

namespace SchoolWebInternalAPI.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string fullName);
        Task<AuthResponseDto?> RefreshAsync(string refreshToken);
        Task<bool> RevokeAsync(string refreshToken);
    }
}
