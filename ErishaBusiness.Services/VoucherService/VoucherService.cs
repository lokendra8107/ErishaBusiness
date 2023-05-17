using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class VoucherService : IVoucherService
    {
        IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<ProductVoucherDetailDto> GetAllProductVoucher(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var result = await _voucherRepository.GetAll(Id, SkipRecord, TakeRecord, SortColumn, SortColumnDirection, SearchValue);
            return result;
        }

        public async Task<int> DeleteProductVoucherById(int Id)
        {
            var result = await _voucherRepository.DeleteCategoryById(Id);
            return result;
        }

        public async Task<ProductVoucherDto> GetProductVoucherByID(int Id)
        {
            var result = await _voucherRepository.GetProductVoucherById(Id);
            return result;
        }

        public int InsertUpdateProductVoucher(ProductVoucherDto objCategory)
        {
            var result = _voucherRepository.InsertUpdateProductVoucher(objCategory);
            return result;
        }
    }
}
