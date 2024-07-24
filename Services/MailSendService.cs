﻿using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendMail.Models;

namespace SendMail.Services
{
    public class MailSendService :IMailSendService
    {
        private readonly MailSettings _mailSettings;
        public MailSendService(IOptions<MailSettings> mailSettings) 
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendMailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToMail));
            email.Subject=mailRequest.Subject;
            var builder = new BodyBuilder();

            //sending attachment
            if (mailRequest.Attachments != null)
            {
                byte[] filesbytes;
                foreach(var file in mailRequest.Attachments)
                {
                    if (file.Length > 0 )
                    {
                        using(var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            filesbytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.Name,filesbytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smpt = new SmtpClient();
            smpt.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smpt.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smpt.SendAsync(email);
            smpt.Disconnect(true);

        }
    }

    public interface IMailSendService
    {
    }
}
