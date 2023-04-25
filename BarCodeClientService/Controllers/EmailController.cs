using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using BarCodeClientService.Models;

namespace BarCodeClientService.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult SendFile()
        {
            return View();
        }

        public IActionResult FormEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult index(Email em)
        {
            string mailTo = em.MailTo;
            string subject = em.Subject;
            string body = em.Body;

            MailMessage mm = new MailMessage();
            mm.To.Add(mailTo);
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new MailAddress("porpichit602@gmail.com");
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("porpichit602@gmail.com", "#June199o#");
            smtp.Send(mm);
            ViewBag.message = "The Mail Has Benn Send To " + em.MailTo + " Successfully..";

            return View();
        }

    }
}
