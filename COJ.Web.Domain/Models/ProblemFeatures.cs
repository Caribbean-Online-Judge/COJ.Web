namespace COJ.Web.Domain.Models;

public class ProblemFeatures
{
    public ProblemDetailsStatistics Statistics { get; set; }

    public string Title { get; set; }
    public int Id { get; set; }
    public string Description { get; set; }
    public IList<string> LocalesAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public ProblemDetailsLimits Limits { get; set; }
}

public struct ProblemDetailsStatistics
{
    public int ShippingCount { get; set; }
    public int AcceptedCount { get; set; }
    public double AcceptedPercent { get; set; }
    public double Points { get; set; }
}

public struct ProblemDetailsLimits
{
    public int TotalTimeLimit { get; set; }
    public int TestCaseTimeLimit { get; set; }
    public int MemoryLimit { get; set; }
    public int OutputLimit { get; set; }
    public int SourceCodeLengthLimit { get; set; }
}