using System.ComponentModel;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Entities;

public class ProblemSubmission : BaseEntity
{
    public Account Account { get; set; }
    public Problem Problem { get; set; }
    public SubmissionStatus Status { get; set; }
    [DefaultValue(SubmissionVerdict.None)]
    public SubmissionVerdict Verdict { get; set; }
    public Language Language { get; set; }
    
    public string SourceCode { get; set; }
    
    public double Time { get; set; }
    public long Memory { get; set; }
    public int CpuTime { get; set; }
    public int TestCase { get; set; }
    public int MinTestCase { get; set; }
    public int MaxTestCase { get; set; }
    public int AverageCase { get; set; }
    public bool Lock { get; set; }
    public int AcceptedTests { get; set; }
    public int AcceptedCases { get; set; }
    
    public bool Accepted { get; set; }

    public DateTime? LastJudgingDateTime { get; set; }
}