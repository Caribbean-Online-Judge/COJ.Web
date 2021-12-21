using System.Net.Mime;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Models;
using COJ.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace COJ.Web.API.Controllers;

[Route("v1/submission")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class SubmissionController : ControllerBase
{
    #region Fields

    private readonly ISubmissionService _submissionService;

    #endregion

    #region Constructor

    public SubmissionController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    #endregion

    /// <summary>
    /// Get all submissions using pagination
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    [ProducesDefaultResponseType]
    public IActionResult Get()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// Get a submission
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    [ProducesDefaultResponseType]
    public IActionResult Get(int id)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// Create a new submission
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Post([FromForm] CreateSubmissionRequest request)
    {
        var accountId = HttpContext.GetUserId();

        // At least Source Code File or Source code should have a value
        if (request.SourceCodeFile == null && string.IsNullOrEmpty(request.SourceCode))
            return this.BadRequestWithMessage("At least Source Code File or Source code field should have a value");

        var result = await _submissionService.CreateSubmission(request, accountId);

        if (result.HasError)
            return BadRequest();

        return CreatedAtAction(nameof(Post), new
        {
            Id = result.Value
        });
    }

    /// <summary>
    /// Delete a submission
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    [ProducesDefaultResponseType]
    public IActionResult Delete(int id)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}