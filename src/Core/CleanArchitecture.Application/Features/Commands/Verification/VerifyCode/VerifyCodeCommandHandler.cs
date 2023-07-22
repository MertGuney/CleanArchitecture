namespace CleanArchitecture.Application.Features.Commands.Verification.VerifyCode;

public class VerifyCodeCommandHandler : IRequestHandler<VerifyCodeCommandRequest, ResponseModel<NoContentModel>>
{
    private readonly ICodeService _codeService;

    public VerifyCodeCommandHandler(ICodeService codeService)
    {
        _codeService = codeService;
    }

    public async Task<ResponseModel<NoContentModel>> Handle(VerifyCodeCommandRequest request, CancellationToken cancellationToken)
    {
        return await _codeService.VerifyAsync(request.Email, request.Code, cancellationToken)
            ? await ResponseModel<NoContentModel>.SuccessAsync()
            : await ResponseModel<NoContentModel>.FailureAsync(ErrorCode.VerifyValidationCode, "Verify Validation Code Error", "An error occurred while validating the code.", StatusCodes.Status500InternalServerError);
    }
}
