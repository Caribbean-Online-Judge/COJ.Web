using System.Net.Mime;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace COJ.Web.API.Controllers;

[Route("v1/institution")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class InstitutionController : Controller
{
    private readonly IInstitutionService _institutionService;

    public InstitutionController(IInstitutionService institutionService)
    {
        _institutionService = institutionService;
    }

    [HttpGet]
    [SwaggerOperation("Get all institutions")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PublicInstitutionDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAllInstitution(
        [SwaggerParameter("Use to get all institutions of a country")] [FromQuery] int? countryId)
    {
        var locales = await _institutionService.GetAll(countryId);
        if (locales.HasError)
            return BadRequest();
        return Ok(locales.Value);
    }
}