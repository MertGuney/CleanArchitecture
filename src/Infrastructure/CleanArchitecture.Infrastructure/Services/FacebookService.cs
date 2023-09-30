namespace CleanArchitecture.Infrastructure.Services;

public class FacebookService : IFacebookService
{
    private readonly HttpClient _httpClient;
    private readonly AuthOption _authOptions;

    public FacebookService(HttpClient httpClient, AuthOption authOptions)
    {
        _httpClient = httpClient;
        _authOptions = authOptions;
    }

    public async Task<ExternalVerifyTokenResponse> VerifyTokenAsync(string authToken)
    {
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        string accessTokenResponse = await _httpClient.GetStringAsync($"/oauth/access_token?client_id={_authOptions.Facebook.ClientId}&client_secret={_authOptions.Facebook.ClientSecret}&grant_type=client_credentials");

        FacebookAccessTokenResponse facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse, jsonSerializerOptions);

        string userAccessTokenValidation = await _httpClient.GetStringAsync($"/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidationResponse validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationResponse>(userAccessTokenValidation, jsonSerializerOptions);

        if (validation?.Data.IsValid is not null)
        {
            var userLoginInfo = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            var userInfo = await UserInfoAsync(authToken);
            return new ExternalVerifyTokenResponse() { Name = userInfo.Name, Email = userInfo.Email, LoginInfo = userLoginInfo };
        }
        throw new Exception("Invalid external authentication");
    }

    public async Task<FacebookUserInfoResponse> UserInfoAsync(string authToken)
    {
        string userInfoResponse = await _httpClient.GetStringAsync($"/me?fields=email,name&access_token={authToken}");

        return JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
