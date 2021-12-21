using System.Net.Mime;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[Route("v1/language")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]

public class LanguageController : Controller
{
    private readonly ILanguageService _languageService;

    public LanguageController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    /// <summary>
    /// Get all locales
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Language>>> GetAllLanguages()
    {
        var locales = await _languageService.GetAll();
        if (locales.HasError)
            return BadRequest();
        return Ok(locales.Value);
    }
}