namespace CleanArchitecture.Application.Features.Commands.Auth.ExternalLogin;

public class ExternalLoginCommandRequest : IRequest<ResponseModel<ExternalLoginCommandResponse>>
{
    public string Provider { get; set; }
    public string AuthToken { get; set; }
}
