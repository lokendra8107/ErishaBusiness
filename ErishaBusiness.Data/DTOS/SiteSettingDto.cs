using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class SiteSettingDto
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Favicon { get; set; }
        public string AdminEmail { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string MailFromEmail { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpuserName { get; set; }
        public string SmtpuserPassword { get; set; }
        public string SmtpPort { get; set; }
        public bool IsActive { get; set; }
    }
}
