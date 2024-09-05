using System.Net;
using System.Net.Mail;

namespace PadigalAPI.Services
{
    /// <summary>
    /// Provides methods for sending email messages.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The body of the email, in HTML format.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }


    /// <inheritdoc />
    public class EmailService : IEmailService
    {

        private readonly ILogger<EmailService> _logger;
        private readonly string _yourEmail = ""; // TODO: Add your email address
        private readonly string _yourPassword = ""; // TODO: Add your email password

        /// <summary>
        /// Initializes a new instance of <see cref="EmailService"/> with the specified logger.
        /// </summary>
        /// <param name="logger">The logger to be used for logging.</param>
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_yourEmail, _yourPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_yourEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            return smtpClient.SendMailAsync(mailMessage);
        }
    }

}
