using CleanArchitecture.Application.DTOs.Tokens;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Shared;
using MediatR;

namespace CleanArchitecture.Application.Features.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, ResponseModel<LoginCommandResponse>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseModel<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDTO token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password);

            LoginCommandResponse response = new()
            {
                AccessToken = token.AccessToken,
                AccessTokenExpiration = token.AccessTokenExpiration,
                RefreshToken = token.RefreshToken,
                RefreshTokenExpiration = token.RefreshTokenExpiration,
            };

            return await ResponseModel<LoginCommandResponse>.SuccessAsync(response);
        }
    }
}
