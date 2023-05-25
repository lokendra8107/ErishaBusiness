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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        readonly IDbConnection _dbConnection;

        public CategoryRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<CategoryDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var results = await GetQueryMultipleAsync(DbConstant.GetAllCategory, new
            {
                @Id = Id,
                @SkipRecord = SkipRecord,
                @TakeRecord = TakeRecord,
                @SortColumn = SortColumn,
                @SortColumnDirection = SortColumnDirection,
                @SearchValue = SearchValue
            });
            var categories = new CategoryDetailDto();
            categories.Categories = new List<CategoryDto>();
            categories.Categories = (results.ReadAsync<CategoryDto>()).Result.ToList();
            categories.RowCounts = (results.ReadAsync<int>()).Result.FirstOrDefault();
            return categories;
        }

        public async Task<int> DeleteCategoryById(int Id)
        {
            var results = await _dbConnection.ExecuteAsync(DbConstant.DeleteCategoryById, new
            {
                @Id = Id
            }, commandType: CommandType.StoredProcedure);
            return results;
        }

        public int InsertUpdateCategory(CategoryDto objCategory)
        {
            try
            {
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.InsertUpdateCategory, new
                {
                    @Id = objCategory.Id,
                    @CategoryName = objCategory.CategoryName,
                    @CategoryType = objCategory.CategoryType,
                    @ImagePath = objCategory.ImagePath,
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

        public async Task<CategoryDto> GetCategoryById(int Id)
        {
            var results = await GetQueryFirstAsync<CategoryDto>(DbConstant.GetAllCategory, new
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

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesList()
        {
            var results = await GetAll<CategoryDto>(DbConstant.GetAllCategoryList);
            return results;
        }
    }
}
