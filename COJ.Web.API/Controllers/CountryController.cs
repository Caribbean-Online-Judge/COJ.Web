using System.Net.Mime;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace COJ.Web.API.Controllers;

[Route("v1/country")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class CountryController : Controller
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }
    
    [HttpGet]
    [SwaggerOperation("Get all countries")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PublicCountryDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLocales()
    {
        var locales = await _countryService.GetAll();
        if (locales.HasError)
            return BadRequest();
        return Ok(locales.Value);
    }
}