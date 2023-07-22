namespace CleanArchitecture.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
}
