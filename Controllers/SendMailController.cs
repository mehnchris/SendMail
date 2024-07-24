using MailKit;
using Microsoft.AspNetCore.Mvc;
using SendMail.Interfaces;
using SendMail.Models;
using SendMail.Services;

namespace SendMail.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SendMailController : Controller
    {
        public readonly IMailSendServices _mailService;

        public SendMailController(IMailSendServices mailServices)
        {
            this._mailService = mailServices;
        }

        [HttpPost]
        //provide action to send email
        public async Task<IActionResult> SendEmail([FromForm] MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
