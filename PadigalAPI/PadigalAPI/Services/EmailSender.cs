using System.Net;
using System.Net.Mail;

namespace PadigalAPI.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("renataunda11@gmail.com", "jahd snlq nitf dbue"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("renataunda11@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            return smtpClient.SendMailAsync(mailMessage);
        }
    }

}
