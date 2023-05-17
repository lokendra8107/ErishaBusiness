using Dapper;
using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public class EmailTemplateRepository : BaseRepository, IEmailTemplateRepository
    {
        readonly IDbConnection _dbConnection;

        public EmailTemplateRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<EmailTemplateDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var results = await GetQueryMultipleAsync(DbConstant.GetAllEmailTemplate, new
            {
                @Id = Id,
                @SkipRecord = SkipRecord,
                @TakeRecord = TakeRecord,
                @SortColumn = SortColumn,
                @SortColumnDirection = SortColumnDirection,
                @SearchValue = SearchValue
            });
            var categories = new EmailTemplateDetailDto();
            categories.EmailTemplates = new List<EmailTemplateDto>();
            categories.EmailTemplates = (results.ReadAsync<EmailTemplateDto>()).Result.ToList();
            categories.RowCounts = (results.ReadAsync<int>()).Result.FirstOrDefault();
            return categories;
        }

        public int UpdateEmailTemplate(EmailTemplateDto objCategory)
        {
            try
            {
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.UpdateEmailTemplate, new
                {
                    @Id = objCategory.Id,
                    @EmailTitle = objCategory.EmailTitle,
                    @EmailSubject = objCategory.EmailSubject,
                    @EmailTo = objCategory.EmailTo,
                    @EmailCc = objCategory.EmailCC,
                    @EmailBcc = objCategory.EmailBCC,
                    @EmailBody = objCategory.EmailBody,
                    @AddedBy = objCategory.AddedBy,
                    @EmailFrom = objCategory.EmailFrom,
                    @IsActive = objCategory.IsActive,
                }, commandType: CommandType.StoredProcedure);
                CloseConnection();
                return results;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<EmailTemplateDto> GetEmailTemplateById(int Id)
        {
            var results = await GetQueryFirstAsync<EmailTemplateDto>(DbConstant.GetAllEmailTemplate, new
            {
                @Id = Id,
                @SkipRecord = 0,
                @TakeRecord = 0,
                @SortColumn = "",
                @SortColumnDirection = "",
                @SearchValue = ""
            });
            return results;
        }
    }
}
