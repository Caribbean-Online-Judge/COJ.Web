using COJ.Web.API.Values;
using COJ.Web.Domain;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[ApiController]
[Route("v1/auth")]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private IAuthService _authService;
    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }


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
            var result = await _authService.SignUp(request);

            return Ok(result);
        }
        catch (AccountEmailUsedException)
        {
            return BadRequest(new
            {
                Code = ResponseCodes.EMAIL_IN_USE,
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
            var result = await _authService.SignIn(request, arguments);
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

    /// <summary>
    /// Refresh session token using a provided refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="401">If the refresh token is wrong or was expired.</response>
    /// <response code="200">With a new token, a new refresh token and the duration related of new token.</response>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshToken(request.RefreshToken);

        if (result == null)
            return Unauthorized();
        
        return Ok(result);
    }
}

