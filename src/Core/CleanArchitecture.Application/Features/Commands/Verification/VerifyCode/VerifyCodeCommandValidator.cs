namespace CleanArchitecture.Application.Features.Commands.Verification.VerifyCode;

public class VerifyCodeCommandValidator : AbstractValidator<VerifyCodeCommandRequest>
{
    public VerifyCodeCommandValidator()
    {
        RuleFor(x => x.Code).Length(6);
        RuleFor(x => x.Email).EmailAddress();
    }
}
