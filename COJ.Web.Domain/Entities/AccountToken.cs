using System;
using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Entities;

public class AccountToken : BaseEntity
{
    public Account Account { get; set; }

    public string Token { get; set; }

    public AccountTokenType Type { get; set; }
    public DateTime ExpirationTime { get; set; }
}

