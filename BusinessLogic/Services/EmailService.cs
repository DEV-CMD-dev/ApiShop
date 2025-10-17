using BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            var fromAddress = _config["Email:Smtp:From"];
            var smtpHost = _config["Email:Smtp:Host"];
            var smtpPort = int.Parse(_config["Email:Smtp:Port"]);
            var smtpUser = _config["Email:Smtp:Username"];
            var smtpPass = _config["Email:Smtp:Password"];
            var enableSsl = bool.Parse(_config["Email:Smtp:EnableSsl"]);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = enableSsl
            };

            var message = new MailMessage(fromAddress, to, subject, htmlContent)
            {
                IsBodyHtml = true
            };

            try
            {
                await client.SendMailAsync(message);
                _logger.LogInformation("Email sent to {Email}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", to);
                throw;
            }
        }
    }

}
