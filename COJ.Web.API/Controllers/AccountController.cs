
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace COJ.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAccount()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPut()]
        public IActionResult Put()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
