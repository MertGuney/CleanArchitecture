using CleanArchitecture.Application.Common.Constraints;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Commands.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<NoContentModel>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseModel<NoContentModel>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.Email, request.UserName, request.Password)
                ? await ResponseModel<NoContentModel>.SuccessAsync(StatusCodes.Status201Created)
                : await ResponseModel<NoContentModel>.FailureAsync(ErrorCode.Register, "Register Error.", "An error occurred while user registering the user.", StatusCodes.Status500InternalServerError);
        }
    }
}
