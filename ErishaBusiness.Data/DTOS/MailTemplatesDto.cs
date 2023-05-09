using ErishaBusiness.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class MailTemplatesDto
    {
        public MailTemplatesDto()
        {

        }

        public MailTemplatesDto(EmailTemplate mailTemplates)
        {
            if (mailTemplates != null)
            {
                Id = mailTemplates.Id;
                MailFrom = mailTemplates.EmailFrom;
                MailTo = mailTemplates.EmailTo;
                Cc = mailTemplates.EmailCc;
                Bcc = mailTemplates.EmailBcc;
                Subject = mailTemplates.EmailSubject;
                EmailType = mailTemplates.EmailType;
                EmailContent = mailTemplates.EmailBody;
                IsActive = mailTemplates.IsActive;
                ModifiedDate = mailTemplates.ModifiedDate.Value;
            }
        }

        public int Id { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public byte? EmailType { get; set; }
        public string EmailContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string MailFromName { get; set; }
    }
}
