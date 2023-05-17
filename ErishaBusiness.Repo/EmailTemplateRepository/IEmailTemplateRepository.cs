using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplateDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        int UpdateEmailTemplate(EmailTemplateDto objCategory);
        Task<EmailTemplateDto> GetEmailTemplateById(int Id);
    }
}
