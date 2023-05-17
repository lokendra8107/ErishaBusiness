using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface IVoucherRepository
    {
        Task<ProductVoucherDetailDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteCategoryById(int Id);
        int InsertUpdateProductVoucher(ProductVoucherDto objProductVoucher);
        Task<ProductVoucherDto> GetProductVoucherById(int Id);
    }
}
