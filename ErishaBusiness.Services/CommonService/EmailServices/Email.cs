using ErishaBusiness.Services.CommonServices.EmailServices;
using ErishaBusiness.Services.CommonService.EmailServices.EmailHelper;
using System;
using System.Threading.Tasks;
using ErishaBusiness.Data.DTOS;

namespace ErishaBusiness.Services.CommonService.EmailServices
{
    public class Email
    {
        public async Task<bool> SendEmail(string html, string subject, string email, string replyName)
        {
            try
            {
                FlexiMail mail = new FlexiMail
                {
                    To = email,
                    FromName = replyName,
                    From = "test@mail.com",
                    Subject = subject,
                    MailBody = html,
                    MailBodyManualSupply = true
                };
                mail.Send();
                return await Task.Run(() =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
                return false;
            }
        }

        public async Task<bool> SendEmail(MailTemplatesDto mailTemplate, EmailToken emailToken, string SmtpServer, string MailFromName, string SMTPUserName, string SMTPUserPassword, int SMTPPort)
        {
            try
            {
                FlexiMail mail = new FlexiMail
                {
                    To = emailToken.Email,
                    From = MailFromName,
                    FromName = MailFromName,
                    Subject = mailTemplate.Subject,
                    MailBody = EmailUtility.GetTokenValues(mailTemplate.EmailContent, emailToken),
                    MailBodyManualSupply = true
                };
                mail.Send(SmtpServer, SMTPUserName, SMTPUserPassword, SMTPPort);
                return await Task.Run(() =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
                return false;
            }
        }

        public async Task<bool> SendContactsEmail(MailTemplatesDto mailTemplate, EmailToken emailToken, string SmtpServer, string MailFromName, string SMTPUserName, string SMTPUserPassword, int SMTPPort)
        {
            try
            {
                FlexiMail mail = new FlexiMail
                {
                    To = mailTemplate.MailTo,
                    From = MailFromName,
                    FromName = MailFromName,
                    Subject = mailTemplate.Subject,
                    MailBody = EmailUtility.GetTokenValues(mailTemplate.EmailContent, emailToken),
                    MailBodyManualSupply = true
                };
                mail.Send(SmtpServer, SMTPUserName, SMTPUserPassword, SMTPPort);
                return await Task.Run(() =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
                return false;
            }
        }
       public void SendRegistrationEmail(MailTemplatesDto mailTemplate, EmailToken emailToken, string SmtpServer, string MailFromName, string SMTPUserName, string SMTPUserPassword, int SMTPPort)
        {
            try
            {
                FlexiMail mail = new FlexiMail
                {
                    To = emailToken.Email,
                    From = MailFromName,
                    BCC = mailTemplate.Bcc,
                    CC = mailTemplate.Cc,
                    Subject = mailTemplate.Subject,
                    MailBody = EmailUtility.GetTokenValues(mailTemplate.EmailContent, emailToken),
                    MailBodyManualSupply = true
                };
                mail.Send(SmtpServer, SMTPUserName, SMTPUserPassword, SMTPPort);
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
            }
        }
    }
}
