using COJ.Web.Infrastructure.Extensions;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[Route("v1/account")]
[ApiController]
[Authorize]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccount()
    {
        var userId = HttpContext.GetUserId();

        if (userId == null)
            return Unauthorized();

        var account = await _mediator.Send(new GetAccountByIdQuery
        {
            Id = userId.Value
        });

        return Ok(new
        {
            account.Username,
            account.Nick,
            account.FirstName,
            account.LastName,
            account.Sex,
            account.LastConnectionDate,
            account.Email,
            account.EmailConfirmed,
            account.Birthday,
            account.Language,
            account.Country,
            account.Institution,
            account.Locale,
            account.Permissions,
            account.Settings
        });
    }

    [HttpPut]
    public IActionResult Put()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}