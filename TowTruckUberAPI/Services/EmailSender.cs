using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TowTruckUberAPI.Services
{
    internal static class EmailSender
    {
        internal static string SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("towtruckuber@gmail.com"),
                    Subject = subject,
                    Body = htmlMessage,
                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential("towtruckuber@gmail.com", "P5fq46xNmArWUcj"),
                };
                client.Send(mail);

                return "Email Sent Successfully!";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }
        }
    }
}
