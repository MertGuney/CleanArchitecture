using CleanArchitecture.Application.Features.Commands.Auth.Login;
using CleanArchitecture.Application.Features.Commands.Auth.Register;
using CleanArchitecture.Application.Features.Commands.Auth.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request)
            => ActionResultInstance(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
            => ActionResultInstance(await _mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommandRequest request)
            => ActionResultInstance(await _mediator.Send(request));
    }
}
