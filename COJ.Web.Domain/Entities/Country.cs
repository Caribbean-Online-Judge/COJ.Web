namespace COJ.Web.Domain.Entities;
public class Country : BaseEntity
{
    /// <summary>
    /// Name of the country.
    /// </summary>
    public string Name { get; set; }
    public string ISOCode { get; set; }
    /// <summary>
    /// Indicate if the country is enabled.
    /// </summary>
    public bool Enabled { get; set; }
}