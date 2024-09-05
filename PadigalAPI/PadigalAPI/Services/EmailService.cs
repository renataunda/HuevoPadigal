using System.Net;
using System.Net.Mail;

namespace PadigalAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(YourEmail, YourPassword), // TODO add email and password
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
