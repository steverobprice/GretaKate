using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace GretaKate.Services
{
    public interface IEmailService
    {
        bool SendEmail(string toEmailAddress, string toFullName, string fromAddress, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public bool SendEmail(string toEmailAddress, string toFullName, string fromAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.To.Add(new MailAddress(toEmailAddress));
            message.From = new MailAddress(fromAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 15;

            client.Send(message);

            return true;
        }
    }
}
