namespace COJ.Web.Domain.Entities;

public class AccountPermission : BaseEntity
{
    public string Permission { get; set; }


    public static explicit operator string(AccountPermission source)
    {
        return source.Permission;
    }

}