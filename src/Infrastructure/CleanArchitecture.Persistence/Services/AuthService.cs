using CleanArchitecture.Application.Contracts.Responses;
using CleanArchitecture.Application.Contracts.Responses.Externals.Facebook;
using Google.Apis.Auth;
using System.Text.Json;

namespace CleanArchitecture.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ICodeService _codeService;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(HttpClient httpClient, ICodeService codeService, ITokenService tokenService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _httpClient = httpClient;
        _codeService = codeService;
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    //public async Task<TokenResponse> ExternalLoginAsync(User user, string provider)
    //{
    //    UserLoginInfo loginInfo;
    //    switch (provider)
    //    {
    //        case "FACEBOOK":
    //            loginInfo = await VerifyFacebookTokenAsync("");
    //        default:
    //            break;
    //    }
    //}

    public async Task<TokenResponse> LoginAsync(string userNameOrEmail, string password, bool rememberMe)
    {
        User user = await _userManager.FindByNameAsync(userNameOrEmail);
        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user is null)
            {
                throw new NotFoundException("Invalid Email/UserName Or Password");
            }
        }

        // SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        SignInResult result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        if (result.Succeeded)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return _tokenService.CreateAccessToken(user, userRoles);
        }
        throw new CustomApplicationException("Invalid Email/UserName Or Password");
    }

    public async Task<UserLoginInfo> VerifyFacebookTokenAsync(string authToken)
    {
        //string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");
        string accessTokenResponse = await _httpClient.GetStringAsync("");

        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        FacebookAccessTokenResponse facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse, jsonSerializerOptions);

        string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidationResponse validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationResponse>(userAccessTokenValidation, jsonSerializerOptions);

        if (validation?.Data.IsValid is not null)
        {
            string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponse userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse, jsonSerializerOptions);

            return new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
        }
        throw new Exception("Invalid external authentication");
    }

    public async Task<UserLoginInfo> VerifyGoogleTokenAsync(string idToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { string.Empty }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            if (payload is null)
                throw new Exception("Invalid external authentication");

            return new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<bool> RegisterAsync(string email, string userName, string password)
    {
        var existUser = await _userManager.FindByEmailAsync(email);
        if (existUser is not null) throw new CustomApplicationException("Exist User");

        User user = new()
        {
            Email = email,
            UserName = userName
        };

        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<bool> ResetPasswordAsync(string email, string code, string newPassword, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            var verifyCode = await _codeService.IsVerifiedAsync(user.Id, code, cancellationToken);
            if (!verifyCode) throw new CustomApplicationException("Verification code could not be verified.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded) throw new CustomApplicationException("Reset Password Exception");

            IdentityResult securityResult = await _userManager.UpdateSecurityStampAsync(user);
            if (!securityResult.Succeeded) throw new CustomApplicationException("Security Stamp Result");

            return true;
        }
        throw new NotFoundException("User Not Found");
    }
}
