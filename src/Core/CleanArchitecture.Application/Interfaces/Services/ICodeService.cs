namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface ICodeService
    {
        Task<bool> SendAsync(string email, CancellationToken cancellationToken);
        Task<string> GenerateAsync(string userId, CancellationToken cancellationToken);
        Task<bool> VerifyAsync(string email, string code, CancellationToken cancellationToken);
        Task<bool> IsVerifiedAsync(string userId, string code, CancellationToken cancellationToken);
    }
}
