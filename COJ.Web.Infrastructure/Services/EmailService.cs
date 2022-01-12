using System.Diagnostics;
using COJ.Web.Domain.Abstract;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Environment;
using MimeKit;
using Serilog;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace COJ.Web.Infrastructure.Services;

public sealed class EmailService : IEmailService
{
    private readonly AppEnvironment _environment;
    private const string PrincipalLinkPlaceholder = "[%LINK%]";

    private readonly string _appDomain;
    private readonly MailboxAddress _emailFrom;

    public EmailService(AppEnvironment environment)
    {
        _environment = environment;
        _appDomain = environment.AppDomain;
        _emailFrom = new MailboxAddress("Caribbean Online Judge", _environment.FromMailAddress);
    }


    public async Task<bool> SendAccountConfirmation(string toEmail, string token)
    {
        try
        {
            var emailTemplate = GetTemplate(EmailTemplates.AccountConfirmation);
            var link = $"https://{_appDomain}/api/v1/account/confirm?email={toEmail}&token={token}";
            var body = emailTemplate.Replace(PrincipalLinkPlaceholder, link);

            var message = BuildMessage(toEmail, "COJ account confirmation", body);

            return await SendMessage(message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, LogsTags.EMAIL_SERVICE_FATAL_ERROR);
            throw;
        }
    }

    private async Task<bool> SendMessage(MimeMessage message)
    {
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_environment.SmtpHost, _environment.SmtpPort, _environment.SmtpSsl);

            await client.AuthenticateAsync(_environment.SmtpUsername, _environment.SmtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex, LogsTags.EMAIL_SERVICE_FATAL_ERROR);
            throw;
        }
    }
    private MimeMessage BuildMessage(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(_emailFrom);
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = "COJ account confirmation";

        message.Body = new TextPart("html")
        {
            Text = body
        };

        return message;
    }
    
    private static string GetTemplate(EmailTemplates template)
    {
        var name = template + ".html";
        var path = Path.Combine(AppEnvironment.EmailsTemplatesFolder, "en", name);
        //TODO: implement emails localization
        return File.ReadAllText(path);
    }
}