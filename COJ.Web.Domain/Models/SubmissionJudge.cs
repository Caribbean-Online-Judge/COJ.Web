using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Models;

public class SubmissionJudge
{
    public int ProblemId { get; set; }
    public Guid Id { get; set; }
    public bool Trusted { get; set; }
    public bool AllResults { get; set; }
    public string SourceCode { get; set; }
    public string LanguageName { get; set; }
    public EvaluationType EvaluationType { get; set; }
    public long SourceCodeLengthLimit { get; set; }
    public long OutputLimit { get; set; }
    public long CaseTimeLimit { get; set; }
    public long TimeLimit { get; set; }
    public long MemoryLimit { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}