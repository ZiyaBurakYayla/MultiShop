using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MultiShop.Mail.Dtos;
using MultiShop.Mail.Settings;

namespace MultiShop.Mail.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<MailService> _logger;

        public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public void SendMail(SendMailDto sendMailDto)
        {
            var fromAddress = string.IsNullOrWhiteSpace(_mailSettings.From) ? "no-reply@multishop.com" : _mailSettings.From;

            var message = new MailMessage();
            message.From = new MailAddress(fromAddress, "MultiShop");
            message.To.Add(sendMailDto.To);
            message.Subject = sendMailDto.Subject;
            message.Body = sendMailDto.Body;
            message.IsBodyHtml = true;

            var client = new SmtpClient();

            if (string.IsNullOrWhiteSpace(_mailSettings.Host) || string.IsNullOrWhiteSpace(_mailSettings.Username))
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "SentMails");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = folder;
                client.Send(message);
                _logger.LogInformation("Mail gönderildi (yerel klasör): {Folder} - Alıcı: {To}", folder, sendMailDto.To);
                return;
            }

            client.Host = _mailSettings.Host;
            client.Port = _mailSettings.Port;
            client.Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password);
            client.EnableSsl = _mailSettings.EnableSsl;
            client.Send(message);
            _logger.LogInformation("Mail gönderildi (SMTP): Alıcı: {To}", sendMailDto.To);
        }
    }
}
