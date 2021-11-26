namespace COJ.Web.Domain.Values
{
    public enum AccountTokenType
    {
        PasswordReset = 0,
        EmailConfirmation = 1,
        EmailChange = 2,
        PhoneConfirmation = 3,
        PhoneChange = 4,
        TwoFactorAuthentication = 5,
        Lockout = 6
    }
}