using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            string res = "";
            if (string.IsNullOrWhiteSpace(res))
            {
                throw new NotImplementedException();
            }
            return Ok();
        }
    }
}
