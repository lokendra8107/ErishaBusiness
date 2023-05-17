using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class EmailTemplateDetailDto
    {
        public int RowCounts { get; set; }
        public List<EmailTemplateDto> EmailTemplates { get; set; }
    }
    public partial class EmailTemplateDto
    {
        public int Id { get; set; }
        public string EmailTitle { get; set; }
        public string EmailSubject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailBody { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid AddedBy { get; set; }
    }
}
