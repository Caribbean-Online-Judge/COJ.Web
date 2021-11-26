using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;
using COJ.Web.Infraestructure.Exceptions;
using COJ.Web.Infraestructure.MediatR.Commands;

using MediatR;

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

