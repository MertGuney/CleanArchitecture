namespace CleanArchitecture.Application.Features.Commands.Auth.Login;

public class LoginCommandRequest : IRequest<ResponseModel<LoginCommandResponse>>
{
    public bool RememberMe { get; set; }
    public string Password { get; set; }
    public string UserNameOrEmail { get; set; }
}
