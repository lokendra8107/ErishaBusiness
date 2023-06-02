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
    public class CmsPageRepository : BaseRepository, ICmsPageRepository
    {
        readonly IDbConnection _dbConnection;

        public CmsPageRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<CmsPageDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            try
            {
                var results = await GetQueryMultipleAsync(DbConstant.GetAllCmsPages, new
                {
                    @Id = Id,
                    @SkipRecord = SkipRecord,
                    @TakeRecord = TakeRecord,
                    @SortColumn = SortColumn,
                    @SortColumnDirection = SortColumnDirection,
                    @SearchValue = SearchValue
                });
                var categories = new CmsPageDetailDto();
                categories.CmsPages = new List<CmsPageDto>();
                categories.CmsPages = (results.ReadAsync<CmsPageDto>()).Result.ToList();
                categories.RowCounts = (results.ReadAsync<int>()).Result.FirstOrDefault();
                return categories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteDetailById(int Id)
        {
            var results = await _dbConnection.ExecuteAsync(DbConstant.DeleteCmsPageById, new
            {
                @Id = Id
            }, commandType: CommandType.StoredProcedure);
            return results;
        }

        public int InsertUpdateDetail(CmsPageDto objInsertUpdate)
        {
            try
            {
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.InsertUpdateCmsPage, new
                {
                    @Id = objInsertUpdate.Id,
                    @Name = objInsertUpdate.Name,
                    @Title = objInsertUpdate.Title,
                    @Url = objInsertUpdate.Url,
                    @MetaTitle = objInsertUpdate.MetaTitle,
                    @MetaDescription = objInsertUpdate.MetaDescription,
                    @MetaKeyword = objInsertUpdate.MetaKeyword,
                    @PageContent = objInsertUpdate.PageContent,
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

        public async Task<CmsPageDto> GetDetailById(int Id)
        {
            var results = await GetQueryFirstAsync<CmsPageDto>(DbConstant.GetAllCmsPages, new
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

		public async Task<IEnumerable<CmsPageDto>> GetAllCmsPageList()
		{
			var results = await GetAll<CmsPageDto>(DbConstant.GetAllCmsPageList);
			return results;
		}
	}
}
