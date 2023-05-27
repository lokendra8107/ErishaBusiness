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
    public class BannerLayoutRepository : BaseRepository, IBannerLayoutRepository
    {
        readonly IDbConnection _dbConnection;

        public BannerLayoutRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BannerLayoutDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            try
            {
                var results = await GetQueryMultipleAsync(DbConstant.GetAllBannerLayoutDetail, new
                {
                    @Id = Id,
                    @SkipRecord = SkipRecord,
                    @TakeRecord = TakeRecord,
                    @SortColumn = SortColumn,
                    @SortColumnDirection = SortColumnDirection,
                    @SearchValue = SearchValue
                });
                var categories = new BannerLayoutDetailDto();
                categories.BannerLayouts = new List<BannerLayoutDto>();
                categories.BannerLayouts = (results.ReadAsync<BannerLayoutDto>()).Result.ToList();
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
            var results = await _dbConnection.ExecuteAsync(DbConstant.DeleteBannerLayoutDetailById, new
            {
                @Id = Id
            }, commandType: CommandType.StoredProcedure);
            return results;
        }

        public int InsertUpdateDetail(BannerLayoutDto objInsertUpdate)
        {
            try
            {
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.InsertUpdateBannerLayoutDetail, new
                {
                    @Id = objInsertUpdate.Id,
                    @Title = objInsertUpdate.Title,
                    @ImageUrl = objInsertUpdate.ImageUrl,
                    @AddedBy = objInsertUpdate.AddedBy,
                    @LayoutType = objInsertUpdate.LayoutType,
                    @CategoryId = objInsertUpdate.CategoryId,
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

        public async Task<BannerLayoutDto> GetDetailById(int Id)
        {
            var results = await GetQueryFirstAsync<BannerLayoutDto>(DbConstant.GetAllBannerLayoutDetail, new
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

        public async Task<IEnumerable<BannerLayoutCategoryDto>> GetCategories()
        {
            var results = await GetAll<BannerLayoutCategoryDto>(DbConstant.GetCategories);
            return results;
        }

        public AllRecordDateModifiedDetailDto GetAllRecordDateModifiedDetail()
        {
            var results = GetQueryFirst<AllRecordDateModifiedDetailDto>(DbConstant.GetAllItemModifiedDate);
            return results;
        }

		public async Task<IEnumerable<BannerLayoutListDto>> GetAllBannerLayout()
		{
			var results = await GetAll<BannerLayoutListDto>(DbConstant.GetAllBannerLayout);
			return results;
		}
	}
}
