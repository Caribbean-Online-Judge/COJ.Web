using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace COJ.Web.Domain.Models;

public class CreateProblemRequest
{
    /// <summary>
    /// Title of the problem.
    /// </summary>
    [Required]
    public string Title { get; set; }
    /// <summary>
    /// Description of the problem using Markdown syntax.
    /// </summary>
    [Required]
    public string Description { get; set; }
    /// <summary>
    /// Problem author.
    /// </summary>
    [Required]
    public string Author { get; set; }
    /// <summary>
    /// Points to win when the problem is accepted.
    /// </summary>
    [Required]
    public double Points { get; set; }
    /// <summary>
    /// Time limit.
    /// </summary>
    [Required]
    public int TimeLimit { get; set; }
    [Required]
    public int MemoryLimit { get; set; }
    [Required]
    public int CaseTimeLimit { get; set; }
    /// <summary>
    /// Output limit (Mb).
    /// </summary>
    [Required]
    public int OutputLimit { get; set; }
    [Required]
    public int SizeLimit { get; set; }
    [Required]
    public bool Multidata { get; set; }
    [Required]
    public bool SpecialJudge { get; set; }

    /// <summary>
    /// Problem classification id.
    /// </summary>
    [Required]
    public int ClassificationId { get; set; }

    public IList<IFormFile> InputFiles { get; set; }
    public IList<IFormFile> OutputFiles { get; set; }
}