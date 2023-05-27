using FluentValidation;

namespace CleanArchitecture.Application.Features.Commands.Users.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.UserName).MinimumLength(8);
        }
    }
}
