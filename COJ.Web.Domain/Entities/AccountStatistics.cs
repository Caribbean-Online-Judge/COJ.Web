namespace COJ.Web.Domain.Entities;

/// <summary>
/// Related statistics of a problem
/// </summary>
public class AccountStatistic : BaseEntity
{
    public int Accepted { get; set; }
    public int WrongAnswer { get; set; }
    public int CompilationError { get; set; }
    public int RuntimeError { get; set; }
    public int TimeLimit { get; set; }
    public int MemoryLimit { get; set; }
    public int FLE { get; set; }
    public int OutputLimit { get; set; }
    public int Ole { get; set; }
    public int PresentationError { get; set; }
    public int SV { get; set; }
    public int Uq { get; set; }
    public int accu { get; set; }
    public int Ivf { get; set; }
    public int Shipping { get; set; }

    public object GetPublicProjection()
    {
        return new
        {
            Accepted,
            WrongAnswer,
            CompilationError,
            RuntimeError,
            TimeLimit,
            MemoryLimit,
            FLE,
            OutputLimit,
            Ole,
            PresentationError,
            SV,
            Uq,
            accu,
            Ivf,
            Shipping
        };
    }
}

