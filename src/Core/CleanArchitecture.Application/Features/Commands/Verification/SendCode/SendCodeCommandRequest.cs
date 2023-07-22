namespace CleanArchitecture.Application.Features.Commands.Verification.SendCode;

public class SendCodeCommandRequest : IRequest<ResponseModel<NoContentModel>>
{
    public string Email { get; set; }
}
