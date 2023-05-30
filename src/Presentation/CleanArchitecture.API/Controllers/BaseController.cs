using CleanArchitecture.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [NonAction]
        public static IActionResult ActionResultInstance<T>(ResponseModel<T> response)
            => new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
    }
}
