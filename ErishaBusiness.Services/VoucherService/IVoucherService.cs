using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface IVoucherService
    {
        Task<ProductVoucherDetailDto> GetAllProductVoucher(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteProductVoucherById(int Id);
        Task<ProductVoucherDto> GetProductVoucherByID(int Id);
        int InsertUpdateProductVoucher(ProductVoucherDto objCategory);
        Task<IEnumerable<ProductVoucherDto>> GetAllVouchersList();
    }
}
