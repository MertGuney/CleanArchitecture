namespace CleanArchitecture.Application.Features.Commands.Auth.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.UserName).MinimumLength(8);
    }
}
