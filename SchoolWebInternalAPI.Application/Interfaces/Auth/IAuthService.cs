namespace SchoolWebInternalAPI.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string fullName);
    }
}
