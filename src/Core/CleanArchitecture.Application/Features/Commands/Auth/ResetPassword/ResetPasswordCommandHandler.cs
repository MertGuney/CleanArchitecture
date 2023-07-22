namespace CleanArchitecture.Application.Features.Commands.Auth.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResponseModel<NoContentModel>>
{
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<ResponseModel<NoContentModel>> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        return await _authService.ResetPasswordAsync(request.Email, request.Code, request.NewPassword, cancellationToken)
            ? await ResponseModel<NoContentModel>.SuccessAsync(StatusCodes.Status204NoContent)
            : await ResponseModel<NoContentModel>.FailureAsync(ErrorCode.ChangePassword, "Change Password Error", "An error occurred while changing the password.", StatusCodes.Status500InternalServerError);
    }
}
