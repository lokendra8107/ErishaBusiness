using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class ProductDetailItemDto
    {
        public int RowCounts { get; set; }
        public List<ProductDetailDto> ProductDetails { get; set; }
    }

    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool IsActive { get; set; }
        public string SizeChart { get; set; }
        public string MaterialName { get; set; }
        public string ProductType { get; set; }
        public bool NewArrival { get; set; }
        public bool BestSelling { get; set; }
        public bool TopBrand { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public int? StarRating { get; set; }
        public string ProductColor { get; set; }
        public int? Quantity { get; set; }
        public int? GSTPrice { get; set; }
        public string SKUId { get; set; }
        public string ProductBreadth { get; set; }
        public string ProductHeight { get; set; }
        public string ProductWeight { get; set; }
        public int? ProductComboSet { get; set; }
        public int? ProductPrice { get; set; }
        public int? ProductPrice_S { get; set; }
        public int? ProductPrice_M { get; set; }
        public int? ProductPrice_L { get; set; }
        public int? ProductPrice_XL { get; set; }
        public int? ProductPrice_XXL { get; set; }
        public int? ProductPrice_Combo { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductImageDetail { get; set; }
        public List<ProductImageDetailDto> ProductImageItem { get; set; }
    }

    public class ProductImageDetailDto
    {
        public int Id { get; set; }
        public string ProductImageUrl { get; set; }
    }

    public class ProductImagesDto
    {
        public string ProductImageUrl { get; set; }
    }
}
