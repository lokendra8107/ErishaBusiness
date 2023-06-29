using Dapper;
using ErishaBusiness.Data.DTOS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ProductDetailItemDto> GetAll(int Id, int SkipRecord, int TakeRecord, string SortColumn, string SortColumnDirection, string SearchValue)
        {
            var results = await GetQueryMultipleAsync(DbConstant.GetAllProductItem, new
            {
                @Id = Id,
                @SkipRecord = SkipRecord,
                @TakeRecord = TakeRecord,
                @SortColumn = SortColumn,
                @SortColumnDirection = SortColumnDirection,
                @SearchValue = SearchValue
            });
            var products = new ProductDetailItemDto();
            products.ProductDetails = new List<ProductDetailDto>();
            products.ProductDetails = (results.ReadAsync<ProductDetailDto>()).Result.ToList();
            products.RowCounts = (results.ReadAsync<int>()).Result.FirstOrDefault();
            return products;
        }

        public async Task<int> DeleteProductById(int Id)
        {
            var results = await _dbConnection.ExecuteAsync(DbConstant.DeleteProductById, new
            {
                @Id = Id
            }, commandType: CommandType.StoredProcedure);
            return results;
        }

        public int InsertUpdateProduct(ProductDetailDto objProducts)
        {
            try
            {
                var packageImageJson = (objProducts.ProductImageDetail != null && objProducts.ProductImageDetail.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(objProducts.ProductImageDetail) : "");
                OpenConnection();
                var results = _dbConnection.Execute(DbConstant.InsertUpdateProductMaster, new
                {
                    @Id = objProducts.Id,
                    @ProductName = objProducts.ProductName,
                    @CategoryId = objProducts.CategoryId,
                    @ProductImage = objProducts.ProductImage,
                    @Description = objProducts.Description,
                    @IsActive = objProducts.IsActive,
                    @SizeChart = objProducts.SizeChart,
                    @MaterialName = objProducts.MaterialName,
                    @NewArrival = objProducts.NewArrival,
                    @BestSelling = objProducts.BestSelling,
                    @TopBrand = objProducts.TopBrand,
                    @MetaTitle = objProducts.MetaTitle,
                    @MetaDescription = objProducts.MetaDescription,
                    @MetaKeyword = objProducts.MetaKeyword,
                    @ProductColor = objProducts.ProductColor,
                    @Quantity = objProducts.Quantity,
                    @GSTPrice = objProducts.GSTPrice,
                    @SKUId = objProducts.SKUId,
                    @ProductBreadth = objProducts.ProductBreadth,
                    @ProductHeight = objProducts.ProductHeight,
                    @ProductWeight = objProducts.ProductWeight,
                    @ProductComboSet = objProducts.ProductComboSet,
                    @ProductPrice = objProducts.ProductPrice,
                    @ProductPrice_S = objProducts.ProductPrice_S,
                    @ProductPrice_M = objProducts.ProductPrice_M,
                    @ProductPrice_L = objProducts.ProductPrice_L,
                    @ProductPrice_XL = objProducts.ProductPrice_XL,
                    @ProductPrice_XXL = objProducts.ProductPrice_XXL,
                    @ProductPrice_Combo = objProducts.ProductPrice_Combo,
                    @PackageImageJson = packageImageJson
                }, commandType: CommandType.StoredProcedure);
                CloseConnection();
                return results;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<ProductDetailDto> GetProductById(int Id)
        {
            var results = await GetQueryFirstAsync<ProductDetailDto>(DbConstant.GetAllProductItem, new
            {
                @Id = Id,
                @SkipRecord = 0,
                @TakeRecord = 0,
                @SortColumn = "",
                @SortColumnDirection = "",
                @SearchValue = ""
            });

            results.ProductImageItem = new List<ProductImageDetailDto>();

            results.ProductImageItem = GetQueryAsync<ProductImageDetailDto>(DbConstant.GetProdutImageItemByProductId, new
            {
                @ProductId = Id
            }).Result.ToList();
            return results;
        }

        public async Task<ProductImageDetailDto> GetProductImageById(int Id)
        {
            var Items = await GetQueryFirstAsync<ProductImageDetailDto>(DbConstant.GetProdutImageItemById, new
            {
                @Id = Id
            });
            return Items;
        }

        public async Task<int> DeleteProductImageById(int Id)
        {
            var results = await _dbConnection.ExecuteAsync(DbConstant.DeleteProdutImageById, new
            {
                @Id = Id
            }, commandType: CommandType.StoredProcedure);
            return results;
        }
    }
}
