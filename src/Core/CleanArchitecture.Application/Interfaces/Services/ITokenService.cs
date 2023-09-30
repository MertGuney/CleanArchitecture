using System.Security.Claims;

namespace CleanArchitecture.Application.Interfaces.Services;

public interface ITokenService
{
    string CreateRefreshToken();

    TokenResponse CreateAccessToken(IEnumerable<Claim> claims);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

    TokenResponse CreateAccessToken(User user, IList<string> userRoles);
}