using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COJ.Web.Domain.Entities;

public class Locale : BaseEntity
{
    public string Description { get; set; }
    public string Code { get; set; }
}


