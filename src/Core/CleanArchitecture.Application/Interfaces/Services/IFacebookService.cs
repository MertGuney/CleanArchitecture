namespace CleanArchitecture.Application.Interfaces.Services;

public interface IFacebookService
{
    Task<FacebookUserInfoResponse> UserInfoAsync(string authToken);
    Task<ExternalVerifyTokenResponse> VerifyTokenAsync(string authToken);
}
