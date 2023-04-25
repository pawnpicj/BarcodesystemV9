using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers.Bank
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class SendEmailController : ControllerBase
    {
        [HttpPost]
        [Route("send-email")]
        public async Task SendEmail([FromBody] JObject objData)
        {
            var message = new MailMessage();            
            message.From = new MailAddress("AutoMail <auto_mail@email.com>");
            message.To.Add(new MailAddress("Netsys Forasus <netsys_asus@outlook.com>"));
            //message.Bcc.Add(new MailAddress("Amit Mohanty <amitmohanty@email.com>"));
            message.Subject = "Test-Subject";
            message.Body = "test";
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
                await Task.FromResult(0);
            }
        }        
    }
}
