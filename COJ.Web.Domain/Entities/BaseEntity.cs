using COJ.Web.Domain.Abstract;

namespace COJ.Web.Domain.Entities;

/// <summary>
/// The base entity.
/// </summary>
public class BaseEntity : IBaseEntity
{
    /// <summary>
    /// Gets or sets the entity identifier
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the entity creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the entity updated date
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}