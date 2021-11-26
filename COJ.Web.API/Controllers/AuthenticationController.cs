using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Models;
using COJ.Web.Infraestructure.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(IAuthService authService)
    {
        AuthService = authService;
    }

    public IAuthService AuthService { get; }

    /// <summary>
    /// Create a new Account.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="400">If there is any validation error, for example, the provided email is used by another account.</response>
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel request)
    {
        try
        {
            var result = await AuthService.SignUp(request);

            return Ok(result);
        }
        catch (AccountEmailUsedException)
        {
            return BadRequest(new
            {
                Code = "EMAIL_IN_USE",
                Message = "The provided email is used!"
            }); ;
        }
    }
}

