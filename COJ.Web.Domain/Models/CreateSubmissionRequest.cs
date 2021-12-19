using Microsoft.AspNetCore.Http;

namespace COJ.Web.Domain.Models;

/// <summary>
/// Represent a new problem submission.
/// </summary>
public class CreateSubmissionRequest
{
    public int ProblemId { get; set; }
    public int LanguageId { get; set; }
    public IFormFile? SourceCodeFile { get; set; }
    public string? SourceCode { get; set; }
    public bool IsForTest { get; set; }
}