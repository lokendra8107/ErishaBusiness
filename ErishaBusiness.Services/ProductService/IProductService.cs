using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public interface IProductService
    {
        Task<ProductDetailItemDto> GetAllProduct(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteProductById(int Id);
        Task<ProductDetailDto> GetProductByID(int Id);
        int InsertUpdateProduct(ProductDetailDto objProduct);
        Task<ProductImageDetailDto> GetProductImageById(int Id);
        Task<int> DeleteProductImageById(int Id);
    }
}
