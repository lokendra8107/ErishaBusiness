using ErishaBusiness.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public interface IProductRepository
    {
        Task<ProductDetailItemDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue);
        Task<int> DeleteProductById(int Id);
        int InsertUpdateProduct(ProductDetailDto objProducts);
        Task<ProductDetailDto> GetProductById(int Id);
        Task<ProductImageDetailDto> GetProductImageById(int Id);
        Task<int> DeleteProductImageById(int Id);
    }
}
