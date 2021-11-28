using COJ.Web.Domain;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.Models;
using COJ.Web.Infraestructure.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
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

    /// <summary>
    /// Create a new session
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="401">If the credentials are wrong</response>
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInModel request)
    {
        try
        {
            var arguments = new SignInArguments()
            {
                IpAddress = Request.GetClientIpAddress()
            };
            var result = await AuthService.SignIn(request, arguments);
            return Ok(result);
        }
        catch (NotAuthorizedException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

