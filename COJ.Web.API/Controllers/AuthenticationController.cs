using COJ.Web.API.Values;
using COJ.Web.Domain;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Models.Dtos;
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
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var result = await _authService.SignUp(request);
        if (result.HasError)
        {
            if (result.Exception is AccountEmailUsedException)
            {
                return BadRequest(new BadRequestResponse
                {
                    Code = ResponseCodes.EmailInUse,
                    Message = "The provided email is used!"
                });
            }
            else
                throw result.Exception;
        }

        return Ok();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Create a new session")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(SignInResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "If the account is disabled")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "If the credentials are invalid or the account is not activated.")]
    public async Task<IActionResult> SignIn([FromBody] SignInModel request)
    {
        var arguments = new SignInArguments
        {
            IpAddress = Request.GetClientIpAddress()
        };
        var result = await _authService.SignIn(request, arguments);
        if (result.HasError)
        {
            return result.Exception switch
            {
                DisabledAccountException => BadRequest(),
                UnauthorizedAccessException => Unauthorized(),
                _ => throw result.Exception,
            };
        }
        return Ok(result.Value);
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

    [HttpPost("recover-password")]
    [AllowAnonymous]
    [SwaggerOperation("Request a recover password token")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
    {
        var result = await _authService.RecoverAccountPassword(request.Email);
        if (result)
            return Ok();
        return BadRequest();
    }
    
    [HttpPost("reset-password")]
    [AllowAnonymous]
    [SwaggerOperation("Reset account password")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetAccountPasswordRequest request)
    {
        var result = await _authService.ResetAccountPassword(request);
        if (result)
            return Ok();
        return Forbid();
    }
}