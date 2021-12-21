using System.Collections;
using System.Net.Mime;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace COJ.Web.API.Controllers;

[Route("v1/locale")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class LocaleController : Controller
{
    private readonly ILocaleService _localeService;
    private readonly ITokenService _tokenService;

    public LocaleController(ILocaleService localeService, ITokenService tokenService)
    {
        _localeService = localeService;
        _tokenService = tokenService;
    }

    [HttpGet]
    [SwaggerOperation("Get all locales", "The locale schema depend of authenticated status.")]
    [SwaggerResponse(StatusCodes.Status200OK, "For anonymous requests", typeof(PublicLocaleDto))]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLocales()
    {
        Result<IEnumerable> result;
        if (HttpContext.IsAnonymousRequest(out var token))
            result = await _localeService.GetAll(true);
        else
        {
            if (_tokenService.ValidateJwtToken(token) && _tokenService.HasRole(token, AccountPermissions.ManageLocale))
                result = await _localeService.GetAll(false);
            else
                return Forbid();
        }

        if (result.HasError)
            return BadRequest();
        return Ok(result.Value);
    }
}