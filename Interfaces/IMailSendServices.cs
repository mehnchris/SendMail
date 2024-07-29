global using SendMail.Models;

namespace SendMail.Interfaces
{
    public interface IMailSendServices
    {
        //set task and pass model which is mailrequest
        public Task SendEmailAsync(MailRequest mailRequest);
    }
}
