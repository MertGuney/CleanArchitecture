namespace CleanArchitecture.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JWTOption _jwtOption;

    public TokenService(IOptions<JWTOption> jwtOption)
    {
        _jwtOption = jwtOption.Value;
    }

    public TokenResponse CreateAccessToken(User user, IList<string> userRoles)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_jwtOption.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.UtcNow.AddSeconds(_jwtOption.RefreshTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_jwtOption.SecurityKey);

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken securityToken = new(
            issuer: _jwtOption.Issuer,
            notBefore: DateTime.UtcNow,
            expires: accessTokenExpiration,
            signingCredentials: signingCredentials,
            claims: GetClaims(user, userRoles)
            );

        JwtSecurityTokenHandler securityTokenHandler = new();

        return new TokenResponse()
        {
            AccessToken = securityTokenHandler.WriteToken(securityToken),
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration,
        };
    }

    public TokenResponse CreateAccessToken(IEnumerable<Claim> claims)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_jwtOption.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.UtcNow.AddSeconds(_jwtOption.RefreshTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_jwtOption.SecurityKey);

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken securityToken = new(
            issuer: _jwtOption.Issuer,
            notBefore: DateTime.UtcNow,
            expires: accessTokenExpiration,
            signingCredentials: signingCredentials,
            claims: claims
            );

        JwtSecurityTokenHandler securityTokenHandler = new();

        return new TokenResponse()
        {
            AccessToken = securityTokenHandler.WriteToken(securityToken),
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration,
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwtOption.Issuer,
            ValidAudiences = _jwtOption.Audiences,
            IssuerSigningKey = SignService.GetSymmetricSecurityKey(_jwtOption.SecurityKey),
        };

        JwtSecurityTokenHandler securityTokenHandler = new();

        ClaimsPrincipal principal = securityTokenHandler
            .ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (securityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }

    private IEnumerable<Claim> GetClaims(User user, IList<string> userRoles)
    {
        List<Claim> claimList = new()
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        claimList.AddRange(userRoles.Select(x => new Claim(JwtClaimTypes.Role, x)));
        claimList.AddRange(_jwtOption.Audiences.Select(x => new Claim(JwtClaimTypes.Audience, x)));
        return claimList;
    }
}