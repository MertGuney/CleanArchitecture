using CleanArchitecture.Shared;
using MediatR;

namespace CleanArchitecture.Application.Features.Commands.Auth.ResetPassword
{
    public class ResetPasswordCommandRequest : IRequest<ResponseModel<NoContentModel>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
