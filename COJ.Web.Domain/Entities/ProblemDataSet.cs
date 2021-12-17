using COJ.Web.Domain.Abstract;

namespace COJ.Web.Domain.Entities;

public class ProblemDataSet : IBaseEntity
{
    public int Id { get; set; }

    public string Input { get; set; }
    public string Output { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}