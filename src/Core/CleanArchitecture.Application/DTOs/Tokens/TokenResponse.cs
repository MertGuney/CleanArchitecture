﻿namespace CleanArchitecture.Application.DTOs.Tokens;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
