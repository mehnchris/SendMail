namespace SendMail.Models
{
    public class MailRequest
    {
        public string EmailSender { get; set; } = string.Empty;

        public string Recipient { get; set; } = string.Empty; 

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        //public List<IFormFile>Attachments { get; set; }
    }
}
