using System.ComponentModel.DataAnnotations;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Exceptions;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[Route("v1/problem")]
[ApiController]
[Produces("application/json")]
public class ProblemController : Controller
{
    private readonly IProblemService _problemService;

    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    /// <summary>
    /// List all problems using pagination.
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AllProblems([FromQuery] PaginationArguments arguments)
    {
        var problems = await _problemService.GetPaginatedProblems(arguments);
        return Ok(problems);
    }

    /// <summary>
    /// Get problem details by problem id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="404">If the problem or problem translation don't exist.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProblemFeatures>> ProblemDetails(int id)
    {
        var locale = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        var result = await _problemService.GetProblemDetailsById(id, locale);
        if (result.HasError)
        {
            return result.Exception switch
            {
                NotExistTranslationException => NotFound(),
                _ => BadRequest()
            };
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get problem statistics.
    /// </summary>
    /// <param name="id">Problem id.</param>
    /// <returns></returns>
    [HttpGet("{id:int}/statistics")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ProblemStatistics(int id)
    {
        var result = await _problemService.GetProblemStatistics(id);
        return result.HasError ? this.BadRequestWithMessage(string.Empty) : Ok(result.Value.GetPublicProjection());
    }

    /// <summary>
    /// Create a new problem.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="400">If the data has any validation issue.</response>
    /// <response code="201"></response>
    [HttpPost]
    [Authorize(Policy = AccountPermissions.CreateProblem)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Problem>> Create([FromForm] CreateProblemRequest request)
    {
        if (request.InputFiles?.Count == 0)
            return BadRequest("Input files can't be empty");

        if (request.OutputFiles?.Count == 0)
            return BadRequest("Output files can't be empty");

        if (request.OutputFiles?.Count != request.InputFiles?.Count)
            return BadRequest("The Input files count must be equal to Output files count");

        var response = await _problemService.CreateProblem(request);
        return CreatedAtAction(nameof(Create), new
        {
            Title = response.GetNeutralTitle(),
            response.Id
        });
    }

    [HttpPatch("{id:int}")]
    [Authorize(Policy = AccountPermissions.UpdateProblem)]
    public IActionResult Edit(int id)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Policy = AccountPermissions.DeleteProblem)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _problemService.DeleteProblemById(id);
        return result ? Ok() : BadRequest();
    }
}