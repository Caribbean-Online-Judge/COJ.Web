using System;

namespace COJ.Web.Domain.Entities;

public class Language : BaseEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Name of the language.
    /// </summary>
    public string Name { get; set; }
    public string Key { get; set; }

    public bool Enabled { get; set; }
    public string Description { get; set; }

    public string Extension { get; set; }
    // TODO: Review it
    public int Aid { get; set; }
    public int Prority { get; set; }
}

