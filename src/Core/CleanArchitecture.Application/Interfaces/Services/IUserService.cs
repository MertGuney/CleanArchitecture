namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> ResetPasswordAsync();
        Task<bool> VerifyResetPasswordTokenAsync(string userId, string token);
    }
}
