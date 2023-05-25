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
    public class VoucherRepository : BaseRepository, IVoucherRepository
    {
        readonly IDbConnection _dbConnection;

        public VoucherRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ProductVoucherDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var results = await GetQueryMultipleAsync(DbConstant.GetAllProductVoucher, new
            {
                @Id = Id,
                @SkipRecord = SkipRecord,
                @TakeRecord = TakeRecord,
                @SortColumn = SortColumn,
                @SortColumnDirection = SortColumnDirection,
                @SearchValue = SearchValue
            });
            var productVouchers = new ProductVoucherDetailDto();
            productVouchers.ProductVouchers = new List<ProductVoucherListDto>();
            productVouchers.ProductVouchers = (results.ReadAsync<ProductVoucherListDto>()).Result.ToList();
            productVouchers.RowCounts = (results.ReadAsync<int>()).Result.FirstOrDefault();
            return productVouchers;
        }

        public async Task<int> DeleteCategoryById(int Id)
        {
            var results = await _dbConnection.ExecuteAsync(DbConstant.DeleteProductVoucherById, new
            {
                @Id = Id
            }, commandType: CommandType.StoredProcedure);
            return results;
        }

        public int InsertUpdateProductVoucher(ProductVoucherDto objProductVoucher)
        {
            try
            {
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.InsertUpdateProductVoucher, new
                {
                    @Id = objProductVoucher.Id,
                    @VoucherCode = objProductVoucher.VoucherCode,
                    @Discount = objProductVoucher.Discount,
                    @StartDate = objProductVoucher.StartDate,
                    @EndDate = objProductVoucher.EndDate,
                    @VoucherType = objProductVoucher.VoucherType,
                    @Description = objProductVoucher.Description,
                    @IsVouncherInPercent = objProductVoucher.IsVouncherInPercent,
                    @IsActive = objProductVoucher.IsActive,
                }, commandType: CommandType.StoredProcedure);
                CloseConnection();
                return results;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<ProductVoucherDto> GetProductVoucherById(int Id)
        {
            var results = await GetQueryFirstAsync<ProductVoucherDto>(DbConstant.GetAllProductVoucher, new
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

        public async Task<IEnumerable<ProductVoucherDto>> GetAllVouchersList()
        {
            var results = await GetAll<ProductVoucherDto>(DbConstant.GetAllVouchersList);
            return results;
        }
    }
}
