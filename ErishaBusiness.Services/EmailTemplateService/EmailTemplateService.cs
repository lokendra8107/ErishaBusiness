using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        IEmailTemplateRepository _emailTemplateRepository;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public async Task<EmailTemplateDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var result = await _emailTemplateRepository.GetAll(Id, SkipRecord, TakeRecord, SortColumn, SortColumnDirection, SearchValue);
            return result;
        }

        public async Task<EmailTemplateDto> GetEmailTemplateByID(int Id)
        {
            var result = await _emailTemplateRepository.GetEmailTemplateById(Id);
            return result;
        }

        public int UpdateEmailTemplate(EmailTemplateDto objEmailTemplate)
        {
            var result = _emailTemplateRepository.UpdateEmailTemplate(objEmailTemplate);
            return result;
        }
    }
}
