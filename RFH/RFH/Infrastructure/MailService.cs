using System.Collections.Generic;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace RFH.Infrastructure
{
    public class MailService
    {
        public void Send(IEnumerable<string> recipients, string subject, string body, string fileAttachment)
        {
            var settings = GetMailSettings();
            
            if (string.IsNullOrEmpty(settings.Smtp.Network.Host))
            {
                // email not configured
                return;
            }

            var mailMessage = new MailMessage
                                  {
                                      From = new MailAddress(settings.Smtp.From),
                                      Subject = subject,
                                      Body = body,
                                      IsBodyHtml = false,
                                      Priority = MailPriority.Normal
                                  };

            foreach (var recipient in recipients)
            {
                mailMessage.To.Add(new MailAddress(recipient));
            }

            if (!string.IsNullOrEmpty(fileAttachment))
            {
                var attachment = new Attachment(fileAttachment);
                mailMessage.Attachments.Add(attachment);
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = settings.Smtp.Network.Host;
                smtpClient.Send(mailMessage);
            }
        }

        protected MailSettingsSectionGroup GetMailSettings()
        {
            var ctx = HttpContext.Current;
            var config = WebConfigurationManager.OpenWebConfiguration(ctx.Request.ApplicationPath);
            var settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            return settings;
        }
    }
}