using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace COJ.Web.Domain.Models;

/// <summary>
/// Represent a new problem submission.
/// </summary>
public class CreateSubmissionRequest
{
    [Required]
    public int ProblemId { get; set; }
    [Required]
    public int LanguageId { get; set; }
    [Required]
    public IFormFile? SourceCodeFile { get; set; }
    [Required]
    public string? SourceCode { get; set; }
    [Required]
    public bool IsForTest { get; set; }
}