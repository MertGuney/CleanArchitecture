using CleanArchitecture.Application.DTOs.Tokens;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string email, string password);
        Task<TokenDTO> LoginAsync(string userNameOrEmail, string password);
        Task<bool> ResetPasswordAsync(string email, string code, string newPassword, CancellationToken cancellationToken);
    }
}
