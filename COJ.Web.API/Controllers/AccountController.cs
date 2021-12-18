using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Extensions;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[Route("v1/account")]
[ApiController]
[Authorize]
[Produces("application/json")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("confirm")]
    [AllowAnonymous]
    public async Task<ActionResult> ConfirmAccount([FromQuery] ConfirmAccountRequest request)
    {
        var result = await _accountService.ConfirmAccount(request);
        return result.HasError
            ? BadRequest(new
            {
                result.Message
            })
            : Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAccount()
    {
        var userId = HttpContext.GetUserId();

        if (userId == null)
            return Unauthorized();

        var account = await _accountService.GetAccountById(userId.Value);

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