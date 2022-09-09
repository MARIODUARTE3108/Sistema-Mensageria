using Microsoft.Extensions.Options;
using Projeto05.Services.Api.Settings;
using System.Net;
using System.Net.Mail;

namespace Projeto05.Services.Api.Helpers
{
    public class EmailHelper
    {
        private readonly MailSettings? _mailSettings;

        public EmailHelper(IOptions<MailSettings>mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public void Send(string mailTo, string subject, string body)
        {
            var mailMessage = new MailMessage(_mailSettings.Conta, mailTo);
            mailMessage.Subject = subject;  
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_mailSettings.Smtp, (int)_mailSettings.Porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_mailSettings.Conta, _mailSettings.Senha);
            smtpClient.Send(mailMessage);
        }
    }
}
