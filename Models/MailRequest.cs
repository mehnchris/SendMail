namespace SendMail.Models
{
    public class MailRequest
    {

        //public string Recipient { get; set; }
        public string ToMail { get; set; } //ToMail

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<IFormFile>Attachments { get; set; }
    }
}
