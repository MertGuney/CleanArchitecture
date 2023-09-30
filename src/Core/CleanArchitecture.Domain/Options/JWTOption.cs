namespace CleanArchitecture.Domain.Options;

public class JWTOption
{
    public string Issuer { get; set; }
    public string SecurityKey { get; set; }
    public List<string> Audiences { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
}