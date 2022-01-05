namespace COJ.Web.Domain.Abstract;

public interface IEmailService
{
    Task<bool> SendAccountConfirmation(string email, string token);
    Task<bool> SendRecoverAccountPassword(string email, string token);
}

