namespace CleanArchitecture.Application.Features.Commands.Verification.VerifyCode;

public class VerifyCodeCommandRequest : IRequest<ResponseModel<NoContentModel>>
{
    public string Code { get; set; }
    public string Email { get; set; }
}
