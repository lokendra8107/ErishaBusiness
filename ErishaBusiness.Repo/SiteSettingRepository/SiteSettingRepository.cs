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
    public class SiteSettingRepository : BaseRepository, ISiteSettingRepository
    {
        readonly IDbConnection _dbConnection;

        public SiteSettingRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public int InsertUpdateDetail(SiteSettingDto objInsertUpdate)
        {
            try
            {
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.UpdateSiteSetting, new
                {
                    @Id = objInsertUpdate.Id,
                    @Logo = objInsertUpdate.Logo,
                    @Favicon = objInsertUpdate.Favicon,
                    @AdminEmail = objInsertUpdate.AdminEmail,
                    @Phone = objInsertUpdate.Phone,
                    @Address = objInsertUpdate.Address,
                    @MailFromEmail = objInsertUpdate.MailFromEmail,
                    @SmtpServer = objInsertUpdate.SmtpServer,
                    @SmtpuserName = objInsertUpdate.SmtpuserName,
                    @SmtpuserPassword = objInsertUpdate.SmtpuserPassword,
                    @SmtpPort = objInsertUpdate.SmtpPort,
                    @IsActive = objInsertUpdate.IsActive,
                }, commandType: CommandType.StoredProcedure);
                CloseConnection();
                return results;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<SiteSettingDto> GetDetailById()
        {
            var results = await GetQueryFirstAsync<SiteSettingDto>(DbConstant.GetSiteSettingById);
            return results;
        }
	}
}
