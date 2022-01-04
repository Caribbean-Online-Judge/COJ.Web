namespace COJ.Web.Infrastructure.Utils;

public class Validator
{
    public const string PasswordRegex =
        "^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8,}$";
}