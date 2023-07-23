using CleanArchitecture.Application.Contracts.Responses.Externals;

namespace CleanArchitecture.Application.Features.Commands.Auth.ExternalLogin;

public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommandRequest, ResponseModel<ExternalLoginCommandResponse>>
{
    private readonly IAuthService _authService;
    private readonly IFacebookService _facebookService;

    public ExternalLoginCommandHandler(IAuthService authService, IFacebookService facebookService)
    {
        _authService = authService;
        _facebookService = facebookService;
    }

    public async Task<ResponseModel<ExternalLoginCommandResponse>> Handle(ExternalLoginCommandRequest request, CancellationToken cancellationToken)
    {
        ExternalVerifyTokenResponse externalResponse = null;
        switch (request.Provider)
        {
            case "FACEBOOK":
                externalResponse = await _facebookService.VerifyTokenAsync(request.AuthToken);
                break;
            case "GOOGLE":
                externalResponse = await _authService.VerifyGoogleTokenAsync(request.AuthToken);
                break;
        }

        var tokenResponse = await _authService.ExternalLoginAsync(externalResponse.Email, externalResponse.LoginInfo);

        ExternalLoginCommandResponse externalLoginCommandResponse = new()
        {
            AccessToken = tokenResponse.AccessToken,
            AccessTokenExpiration = tokenResponse.AccessTokenExpiration,
            RefreshToken = tokenResponse.RefreshToken,
            RefreshTokenExpiration = tokenResponse.RefreshTokenExpiration,
        };
        return await ResponseModel<ExternalLoginCommandResponse>.SuccessAsync(externalLoginCommandResponse);
    }
}
