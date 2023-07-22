namespace CleanArchitecture.Application.Contracts.Responses.Externals.Facebook;

public class FacebookUserAccessTokenValidationResponse
{
    public FacebookUserAccessTokenValidationData Data { get; set; }
}

public class FacebookUserAccessTokenValidationData
{
    public bool IsValid { get; set; }
    public string UserId { get; set; }
}
