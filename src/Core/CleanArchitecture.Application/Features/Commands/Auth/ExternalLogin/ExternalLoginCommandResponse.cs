namespace CleanArchitecture.Application.Features.Commands.Auth.ExternalLogin;

public class ExternalLoginCommandResponse
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
