namespace CleanArchitecture.Application.Interfaces.Services;

public interface ITokenService
{
    string CreateRefreshToken();
    TokenResponse CreateAccessToken(User user, IList<string> userRoles);
}
