using System;
using System.Collections.Generic;

namespace ErishaBusiness.Data.Entities
{
    public partial class EmailTemplate
    {
        public int Id { get; set; }
        public string EmailTitle { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public string EmailBcc { get; set; }
        public string EmailBody { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? AddedBy { get; set; }
        public bool IsActive { get; set; }
        public string EmailFrom { get; set; }
        public byte? EmailType { get; set; }
    }
}
