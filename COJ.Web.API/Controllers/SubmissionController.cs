using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers
{
    [Route("v1/submission")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}
