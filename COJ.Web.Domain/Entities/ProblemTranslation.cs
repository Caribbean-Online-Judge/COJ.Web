using COJ.Web.Domain.Abstract;

namespace COJ.Web.Domain.Entities;

public class ProblemTranslation : IBaseEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public Locale Locale { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}