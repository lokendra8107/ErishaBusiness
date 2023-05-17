using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplateDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<EmailTemplateDto> GetEmailTemplateByID(int Id);
        int UpdateEmailTemplate(EmailTemplateDto objEmailTemplate);
    }
}
