using FluentValidation;

namespace CleanArchitecture.Application.Features.Commands.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {

        }
    }
}
