using CleanArchitecture.Application.DTOs.Tokens;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateRefreshToken();
        TokenDTO CreateAccessToken(User user, IList<string> userRoles);
    }
}
