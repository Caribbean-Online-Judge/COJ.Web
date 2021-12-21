using COJ.Web.API.Values;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace COJ.Web.API.Controllers;

[ApiController]
[Route("v1/auth")]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Sign up")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel request)
    {
        try
        {
            var result = await _authService.SignUp(request);

            return Ok(result);
        }
        catch (AccountEmailUsedException)
        {
            return BadRequest(new BadRequestResponse
            {
                Code = ResponseCodes.EmailInUse,
                Message = "The provided email is used!"
            });
        }
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Create a new session")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn([FromBody] SignInModel request)
    {
        try
        {
            var arguments = new SignInArguments
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
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [SwaggerOperation("Refresh session token using a provided refresh token")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshToken(request.RefreshToken);

        if (result == null)
            return Unauthorized();

        return Ok(result);
    }
}