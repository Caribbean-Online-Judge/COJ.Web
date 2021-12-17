

using COJ.Web.Domain.Abstract;
using System.Net;
using System.Net.Mail;
using COJ.Web.Infrastructure.Environment;

namespace COJ.Web.Infrastructure.Services;

public sealed class EmailService : IEmailService
{
    private readonly MailAddress _emailFrom;
    public EmailService(AppEnvironment environment)
    {
        SmtpClient = new SmtpClient(environment.SmtpHost, environment.SmtpPort);
        SmtpClient.Credentials = new NetworkCredential(environment.SmtpUsername, environment.SmtpPassword);
        _emailFrom = new MailAddress(environment.FromMailAddress);
    }

    public SmtpClient SmtpClient { get; }

    public async Task<bool> SendAccountConfirmation(string email, string token)
    {
        var subject = "Account confirmation";
        var body = $"Confirmation token: {token}";
        var msg = new MailMessage
        {
            From = _emailFrom
        };
        msg.To.Add(email);
        msg.Body = body;
        
#if EMAIL_SEND_ENABLED
         SmtpClient.Send(msg);
#endif
        return true;
    }
}

