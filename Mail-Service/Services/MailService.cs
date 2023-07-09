using Mail_Service.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Mail_Service.Configuration;
using Mail_Service.Dtos;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Mail_Service.Services
{
    public class MailService : IMailService
    {
        public MailService(IOptions<MailSettings> settings)
        {
        }

        public async Task<bool> SendAsync(MailDto mailData, CancellationToken ct = default)
        {
            var host = Environment.GetEnvironmentVariable("SMTP_HOST");
            var port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
            var email = Environment.GetEnvironmentVariable("SMTP_EMAIL");
            var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            var name = Environment.GetEnvironmentVariable("SMTP_NAME");

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(email));
            mail.To.Add(MailboxAddress.Parse(mailData.To));
            mail.Subject = mailData.Subject;
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailData.Body };
            
            

            using var SMTP = new SmtpClient();
            SMTP.Connect(host, port, SecureSocketOptions.StartTls);
            SMTP.Authenticate(email, password);
            SMTP.Send(mail);
            SMTP.Disconnect(true);
            return true;
        }
    }
}