using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        readonly IProductService _productService;
        readonly IBannerLayoutService _bannerLayoutService;

        public ProductController(IProductService ProductService,
            IBannerLayoutService bannerLayoutService)
        {
            _productService = ProductService;
            _bannerLayoutService = bannerLayoutService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadProductData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var ProductData = await _productService.GetAllProduct(0, skip, pageSize, sortColumn, sortColumnDirection, searchValue);
                var products = ProductData.ProductDetails.ToList();
                products.ForEach(x => x.ProductImage = (SiteKeys.AssetsDomain + x.ProductImage));

                recordsTotal = ProductData.RowCounts;
                var data = products;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductById(int Id)
        {
            try
            {
                var getProductData = _productService.GetProductByID(Id).Result;
                if (!string.IsNullOrEmpty(getProductData.ProductImage))
                {
                    string folderPaths = SiteKeys.ImageFolder;
                    string filePaths = $"{folderPaths}{getProductData.ProductImage}";
                    if (System.IO.File.Exists(filePaths))
                    {
                        System.IO.File.Delete(filePaths);
                    }
                }

                var ProductData = await _productService.DeleteProductById(Id);
                return Json(new { status = 1, data = "Product has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, data = "Unable to delete Product." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = new ProductDetailDto();
            if (Id > 0)
            {
                result = await _productService.GetProductByID(Id);
                result.ProductImage = SiteKeys.AssetsDomain + result.ProductImage;
                result.ProductImageItem.ForEach(x => x.ProductImageUrl = SiteKeys.AssetsDomain + x.ProductImageUrl);
            }
            ViewBag.Categories = await _bannerLayoutService.GetCategories();
            return View(result);
        }

        [HttpPost]
        public JsonResult SaveProductData(string data, IFormFile ProductImageFile, List<IFormFile> OtherProductImageFile)
        {
            try
            {
                ProductDetailDto formData = JsonConvert.DeserializeObject<ProductDetailDto>(data);
                var existImage = "";
                if (formData.Id > 0)
                {
                    var ProductData = _productService.GetProductByID(formData.Id).Result;
                    existImage = ProductData.ProductImage;
                }
                if (OtherProductImageFile.Count > 0)
                {
                    formData.ProductImageDetail = new List<string>();
                    foreach (var item in OtherProductImageFile)
                    {
                        string fileExt = Path.GetExtension(item.FileName).ToLower();
                        string fileDataName = Guid.NewGuid() + fileExt;
                        string filefolderPath = SiteKeys.ImageFolder;
                        string fileDatePath = $"{filefolderPath}{fileDataName}";
                        if (!Directory.Exists(filefolderPath))
                        {
                            Directory.CreateDirectory(filefolderPath);
                        }

                        if (fileExt == ".png" || fileExt == ".jpg" || fileExt == ".jpeg")
                        {
                            using (var inputStream = new FileStream(fileDatePath, FileMode.Create))
                            {
                                item.CopyTo(inputStream);
                            }
                            formData.ProductImageDetail.Add(fileDataName);
                        }
                    }
                }

                var ext = "";
                var fileName = "";
                if (ProductImageFile != null)
                {
                    if (!string.IsNullOrEmpty(existImage))
                    {
                        string folderPaths = SiteKeys.ImageFolder;
                        string filePaths = $"{folderPaths}{existImage}";
                        if (System.IO.File.Exists(filePaths))
                        {
                            System.IO.File.Delete(filePaths);
                        }
                    }
                    ext = Path.GetExtension(ProductImageFile.FileName).ToLower();
                    fileName = Guid.NewGuid() + ext;

                    string folderPath = SiteKeys.ImageFolder;
                    string filePath = $"{folderPath}{fileName}";

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        using (var inputStream = new FileStream(filePath, FileMode.Create))
                        {
                            ProductImageFile.CopyTo(inputStream);
                        }
                        formData.ProductImage = fileName;
                        var status = _productService.InsertUpdateProduct(formData);
                        return Json(new { Status = "success", Message = formData.Id == 0 ? "Product has been added" : "Product has been updated" });
                    }
                    else
                    {
                        return Json(new { Status = "failed", Message = "Only png, jpg, jpeg file allowed." });
                    }
                }
                else
                {
                    var status = _productService.InsertUpdateProduct(formData);
                    if (status == 0)
                    {
                        return Json(new { Status = "failed", Message = "Unable to update Product" });
                    }
                    else
                        return Json(new { Status = "success", Message = formData.Id == 0 ? "Product has been added" : "Product has been updated" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = "failed", Message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductImageById(int Id)
        {
            try
            {
                var getProductData = _productService.GetProductImageById(Id).Result;
                if (!string.IsNullOrEmpty(getProductData.ProductImageUrl))
                {
                    string folderPaths = SiteKeys.ImageFolder;
                    string filePaths = $"{folderPaths}{getProductData.ProductImageUrl}";
                    if (System.IO.File.Exists(filePaths))
                    {
                        System.IO.File.Delete(filePaths);
                    }
                }
                var ProductData = await _productService.DeleteProductImageById(Id);
                return Json(new { status = 1, data = "Product Image has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, data = "Unable to delete Product Image." });
            }
        }
    }
}
