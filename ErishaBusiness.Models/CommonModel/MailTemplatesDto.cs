using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ErishaBusiness.Models.Model
{
    public class MailTemplatesDto
    {
        public MailTemplatesDto()
        {

        }

        //public MailTemplatesDto(MailTemplates mailTemplates)
        //{
        //    Id = mailTemplates.Id;
        //    MailFrom = mailTemplates.MailFrom;
        //    MailTo = mailTemplates.MailTo;
        //    Cc = mailTemplates.Cc;
        //    Bcc = mailTemplates.Bcc;
        //    Subject = mailTemplates.Subject;
        //    EmailType = mailTemplates.EmailType;
        //    EmailContent = mailTemplates.EmailContent;
        //    IsActive = mailTemplates.IsActive;
        //    ModifiedDate = mailTemplates.ModifiedDate;  
            
        //}

        public int Id { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public byte EmailType { get; set; }
        public string EmailContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string MailFromName { get; set; }
        public SelectList EmailTypes {get; set;}
        public List<SelectListItem> EmailTypeSelectList { get; set; }
    }
}
