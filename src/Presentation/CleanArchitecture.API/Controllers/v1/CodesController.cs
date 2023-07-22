namespace CleanArchitecture.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class CodesController : BaseController
{
    public CodesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Send(SendCodeCommandRequest request)
        => ActionResultInstance(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> Verify(VerifyCodeCommandRequest request)
        => ActionResultInstance(await _mediator.Send(request));
}
