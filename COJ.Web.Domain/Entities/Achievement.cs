namespace COJ.Web.Domain.Entities;

public class Achievement : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Condition { get; set; }
    public string Legent { get; set; }
    public bool Enabled { get; set; }
}

