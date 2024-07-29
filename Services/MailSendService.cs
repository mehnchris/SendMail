using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendMail.Interfaces;

namespace SendMail.Services
{
    public class MailSendService : IMailSendServices
    {
        public readonly MailSettings _mailSettings;
        public MailSendService(IOptions<MailSettings> mailSettings) 
        {
            _mailSettings= mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.Recipient));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();


            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smpt = new SmtpClient();
            smpt.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smpt.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smpt.SendAsync(email);
            smpt.Disconnect(true);
        }
    }
}
