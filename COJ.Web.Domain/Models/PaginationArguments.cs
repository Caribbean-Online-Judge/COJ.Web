namespace COJ.Web.Domain.Models;

/// <summary>
/// Used to represent pagination arguments.
/// </summary>
public class PaginationArguments
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public string[]? OrderBy { get; set; } = null;
    public string? FilterBy { get; set; } = null;
}