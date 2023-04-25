using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;

namespace BarCodeClientService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            try
            {
                SmtpClient client = new SmtpClient("smtp.office365.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("netsys_asus@outlook.com", "#ABcd/1234");
                client.EnableSsl = true;
                client.Credentials = credential;

                MailMessage message = new MailMessage("netsys_asus@outlook.com", "porpichit602@gmail.com");
                message.Subject = "Test Send Mail";
                message.Body = "<h1>Hi</h1>";
                message.IsBodyHtml = true;
                client.Send(message);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}