namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface ICodeService
    {
        Task<string> GenerateAsync(string userId, CancellationToken cancellationToken);
        Task<bool> VerifyAsync(string userId, string code, CancellationToken cancellationToken);
    }
}
