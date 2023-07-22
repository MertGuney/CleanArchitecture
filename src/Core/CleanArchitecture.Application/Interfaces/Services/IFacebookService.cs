using CleanArchitecture.Application.Contracts.Responses.Externals.Facebook;

namespace CleanArchitecture.Application.Interfaces.Services;

public interface IFacebookService
{
    Task<UserLoginInfo> VerifyTokenAsync(string authToken);
    Task<FacebookUserInfoResponse> UserInfoAsync(string authToken);
}
