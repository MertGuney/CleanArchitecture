namespace CleanArchitecture.Application.Interfaces.Services;

public interface IMailService
{
    Task<bool> SendAsync(string to, string subject, string body);
    Task<bool> SendResetPasswordMailAsync(string to, Guid userId, string token);
    Task<bool> SendForgotPasswordMailAsync(string to, Guid userId, string token);
}
