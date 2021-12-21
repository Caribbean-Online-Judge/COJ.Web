using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace COJ.Web.API.Controllers;

[Route("v1/account")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet("confirm")]
    [AllowAnonymous]
    [SwaggerOperation("Confirm account email")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
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
    [Authorize]
    [SwaggerOperation("Get current account information")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAccount()
    {
        var userId = HttpContext.GetUserId();

        var account = await _accountService.GetAccountById(userId);

        return Ok(new
        {
            account.Username,
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
    
    [HttpPatch]
    [Authorize]
    [SwaggerOperation("Update current account")]
    [SwaggerResponse(StatusCodes.Status501NotImplemented)]
    public IActionResult Update()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    [HttpDelete]
    [Authorize]
    [SwaggerOperation("Delete current account")]
    [SwaggerResponse(StatusCodes.Status501NotImplemented)]
    public IActionResult Delete()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}