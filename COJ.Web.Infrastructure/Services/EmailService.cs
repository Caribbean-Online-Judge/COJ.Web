using System.Diagnostics;
using COJ.Web.Domain.Abstract;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Environment;

namespace COJ.Web.Infrastructure.Services;

public sealed class EmailService : IEmailService
{
    private const string PrincipalLinkPlaceholder = "[%LINK%]";

    private readonly MailAddress _emailFrom;
    private readonly SmtpClient _smtpClient;

    public EmailService(AppEnvironment environment)
    {
        _smtpClient = new SmtpClient(environment.SmtpHost, environment.SmtpPort);
        _smtpClient.EnableSsl = true;
        _smtpClient.Credentials = new NetworkCredential(environment.SmtpUsername, environment.SmtpPassword);
        _emailFrom = new MailAddress(environment.FromMailAddress, "Caribbean Online Judge");
    }


    public async Task<bool> SendAccountConfirmation(string email, string token)
    {
        var emailTemplate = GetTemplate(EmailTemplates.AccountConfirmation);
        var link = $"http://localhost/v1/account/confirm?email={email}&token={token}";
        var body = emailTemplate.Replace(PrincipalLinkPlaceholder, link);
        var msg = new MailMessage
        {
            From = _emailFrom,
            Subject = "Account confirmation"
        };
        msg.To.Add(email);
        msg.IsBodyHtml = true;
        msg.Body = body;

        _smtpClient.Send(msg);
        return await Task.FromResult(true);
    }

    private static string GetTemplate(EmailTemplates template)
    {
        var name = template + ".html";
        var path = Path.Combine(AppEnvironment.EmailsTemplatesFolder, "en", name);
        //TODO: implement emails localization
        return File.ReadAllText(path);
    }
}