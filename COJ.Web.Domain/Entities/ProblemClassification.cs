namespace COJ.Web.Domain.Entities;

public sealed class ProblemClassification : BaseEntity
{
    public string Name { get; set; }
    public int EstimatedCodeLenght { get; set; }
    public double Complexity { get; set; }

    public Achievement Achievement { get; set; }
}

