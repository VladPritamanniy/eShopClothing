using System.Net;
using System.Net.Mail;
using Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
            ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            await Execute(subject, message, toEmail);
        }

        public async Task Execute(string subject, string message, string toEmail)
        {
            MailAddress fromAddress = new MailAddress(Options.FromEmail!, Options.FromDisplayName);
            MailAddress toAddress = new MailAddress(toEmail);

            using (MailMessage letter = new MailMessage(fromAddress, toAddress))
            {
                letter.IsBodyHtml = true;
                letter.Body = message;
                letter.Subject = subject;

                using (SmtpClient smtpClient = new SmtpClient(Options.SmtpHost, Options.SmtpPort))
                {
                    smtpClient.EnableSsl = Options.EnableSsl;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = Options.UseDefaultCredentials;
                    smtpClient.Credentials = new NetworkCredential(fromAddress.Address, Options.FromEmailPassword);

                    try
                    {
                        await smtpClient.SendMailAsync(letter);
                        _logger.LogInformation("Send confirm letter to {toEmail} is succeed", toEmail);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Letter not sent. Sending confirm letter completed with an error: {e}", e);
                    }
                }
            }
        }
    }
}
