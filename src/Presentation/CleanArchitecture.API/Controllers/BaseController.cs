using CleanArchitecture.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public static IActionResult ActionResultInstance<T>(ResponseModel<T> response)
            => new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
    }
}
