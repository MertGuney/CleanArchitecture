namespace CleanArchitecture.Application.Contracts.Responses.Externals;

public class ExternalVerifyTokenResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public UserLoginInfo LoginInfo { get; set; }
}
