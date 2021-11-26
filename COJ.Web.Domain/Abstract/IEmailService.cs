namespace COJ.Web.Domain.Abstract;

public interface IEmailService
{
    Task<bool> SendAccountConfirmation(string email, string token);
}

