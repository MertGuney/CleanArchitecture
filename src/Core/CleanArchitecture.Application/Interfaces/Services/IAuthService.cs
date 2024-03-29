﻿namespace CleanArchitecture.Application.Interfaces.Services;

public interface IAuthService
{
    Task<ExternalVerifyTokenResponse> VerifyGoogleTokenAsync(string idToken);
    Task<bool> RegisterAsync(string email, string userName, string password);
    Task<TokenResponse> ExternalLoginAsync(string email, UserLoginInfo userLoginInfo);
    Task<TokenResponse> LoginAsync(string userNameOrEmail, string password, bool rememberMe);
    Task<bool> ResetPasswordAsync(string email, string code, string newPassword, CancellationToken cancellationToken);
}
