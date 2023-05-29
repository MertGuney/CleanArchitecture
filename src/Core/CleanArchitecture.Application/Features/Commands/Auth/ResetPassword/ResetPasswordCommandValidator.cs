using FluentValidation;

namespace CleanArchitecture.Application.Features.Commands.Auth.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommandRequest>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Code).Length(6);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.NewPassword).NotEmpty();
        }
    }
}
