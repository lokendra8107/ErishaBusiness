using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository ProductRepository)
        {
            _productRepository = ProductRepository;
        }

        public async Task<ProductDetailItemDto> GetAllProduct(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var result = await _productRepository.GetAll(Id, SkipRecord, TakeRecord, SortColumn, SortColumnDirection, SearchValue);
            return result;
        }

        public async Task<int> DeleteProductById(int Id)
        {
            var result = await _productRepository.DeleteProductById(Id);
            return result;
        }

        public async Task<ProductDetailDto> GetProductByID(int Id)
        {
            var result = await _productRepository.GetProductById(Id);
            return result;
        }

        public int InsertUpdateProduct(ProductDetailDto objProduct)
        {
            var result = _productRepository.InsertUpdateProduct(objProduct);
            return result;
        }

        public async Task<ProductImageDetailDto> GetProductImageById(int Id)
        {
            var result = await _productRepository.GetProductImageById(Id);
            return result;
        }

        public async Task<int> DeleteProductImageById(int Id)
        {
            var result = await _productRepository.DeleteProductImageById(Id);
            return result;
        }
    }
}
