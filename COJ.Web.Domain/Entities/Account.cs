using COJ.Web.Domain.Values;

using System.ComponentModel;

namespace COJ.Web.Domain.Entities;

public class Account : BaseEntity
{
    public Account()
    {
        Statistics = new AccountStatistic();
    }
    public string Username { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }


    [DefaultValue(false)]
    public bool Enabled { get; set; }

    public Language? Language { get; set; }

    public Country? Country { get; set; }

    public Institution? Institution { get; set; }

    public Locale? Locale { get; set; }

    public Sex Sex { get; set; }

    public string? LastIpAddress { get; set; }

    public DateTime? LastConnectionDate { get; set; }


    public string? Tags { get; set; }

    public string Email { get; set; }
    [DefaultValue(false)]
    public bool EmailConfirmed { get; set; }
    public DateTime Birthday { get; set; }

    public List<AccountPermission>? Permissions { get; set; }
    public AccountSettings? Settings { get; set; }
    public AccountStatistic Statistics { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
}