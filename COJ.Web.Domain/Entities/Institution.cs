using System;

namespace COJ.Web.Domain.Entities;
public class Institution : BaseEntity
{
    /// <summary>
    /// Name of the institution.
    /// </summary>
    public string Name { get; set; }
    public Country Country { get; set; }
    /// <summary>
    /// Indicate if the institution is enabled.
    /// </summary>
    public bool Enabled { get; set; }
}

