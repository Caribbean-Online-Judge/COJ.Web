using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Environment;
using MailKit.Security;
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
            var link = $"https://{_appDomain}/api/v1/account/confirm?email={toEmail}&token={token}";
            var placeholders = new Dictionary<string, string>
            {
                {PrincipalLinkPlaceholder, link}
            };
            var body = GetTemplate(EmailTemplates.AccountConfirmation, placeholders);

            var message = BuildMessage(toEmail, "COJ account confirmation", body);

            return await SendMessage(message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, LogsTags.EMAIL_SERVICE_FATAL_ERROR);
            throw;
        }
    }

    public async Task<bool> SendRecoverAccountPassword(string toEmail, string token)
    {
        try
        {
            var link = $"https://{_appDomain}/api/v1/auth/reset-password?email={toEmail}&token={token}";
            var placeholders = new Dictionary<string, string>
            {
                {PrincipalLinkPlaceholder, link}
            };
            var body = GetTemplate(EmailTemplates.RecoverPassword, placeholders);

            var message = BuildMessage(toEmail, "COJ password recovery", body);

            return await SendMessage(message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, LogsTags.EmailServiceFatalError);
            throw;
        }
    }

    private async Task<bool> SendMessage(MimeMessage message)
    {
        try
        {
            using var client = new SmtpClient();
            if (_environment.SmtpSsl)
                await client.ConnectAsync(_environment.SmtpHost, _environment.SmtpPort, SecureSocketOptions.StartTls);
            else
                await client.ConnectAsync(_environment.SmtpHost, _environment.SmtpPort);

            await client.AuthenticateAsync(_environment.SmtpUsername, _environment.SmtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return true;
        }
        catch (Exception ex)
        {

            Log.Error(ex, LogsTags.EmailServiceFatalError);
            throw;
        }
    }
    private MimeMessage BuildMessage(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(_emailFrom);
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = body
        };

        return message;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="template"></param>
    /// <param name="placeholders"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    private static string GetTemplate(EmailTemplates template, Dictionary<string, string> placeholders)
    {
        try
        {
            var name = template + ".html";
            var path = Path.Combine(AppEnvironment.EmailsTemplatesFolder, "en", name);
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            //TODO: implement emails localization
            var body = File.ReadAllText(path);
            return placeholders.Aggregate(body, (current, placeholder) => current.Replace(placeholder.Key, placeholder.Value));
        }
        catch (Exception ex)
        {
            Log.Error(ex, LogsTags.EmailServiceTemplatesError);
            throw;
        }
    }
}