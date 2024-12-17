using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class Email
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["Smtp:Username"],
                    _configuration["Smtp:Password"]
                ),
                EnableSsl = true,
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:FromAddress"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception details
                throw new SmtpException("Failed to send email.", ex);
            }
        }

    }
}
