using ErishaBusiness.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using ErishaBusiness.Data.DTOS;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using ErishaBusiness.Core;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadCategoryData()
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

                var categoryData = await _categoryService.GetAllCategory(0, skip, pageSize, sortColumn, sortColumnDirection, searchValue);
                var categories = categoryData.Categories.ToList();
                categories.ForEach(x => x.ImagePath = (SiteKeys.AssetsDomain + x.ImagePath));

                recordsTotal = categoryData.RowCounts;
                var data = categories;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategoryById(int Id)
        {
            try
            {
                var getCategoryData = _categoryService.GetCategoryByID(Id).Result;
                if (!string.IsNullOrEmpty(getCategoryData.ImagePath))
                {
                    string folderPaths = SiteKeys.ImageFolder;
                    string filePaths = $"{folderPaths}{getCategoryData.ImagePath}";
                    if (System.IO.File.Exists(filePaths))
                    {
                        System.IO.File.Delete(filePaths);
                    }
                }

                var categoryData = await _categoryService.DeleteCategoryById(Id);
                return Json(new { status = 1, data = "Category has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, data = "Unable to delete category." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = new CategoryDto();
            if (Id > 0)
            {
                result = await _categoryService.GetCategoryByID(Id);
                result.ImagePath = SiteKeys.AssetsDomain + result.ImagePath;
            }
            return View(result);
        }

        [HttpPost]
        public JsonResult SaveCategoryData(string data, IFormFile categoryImageFile)
        {
            try
            {
                CategoryDto formData = JsonConvert.DeserializeObject<CategoryDto>(data);
                var existImage = "";
                if (formData.Id > 0)
                {
                    var categoryData = _categoryService.GetCategoryByID(formData.Id).Result;
                    existImage = categoryData.ImagePath;
                }

                var ext = "";
                var fileName = "";
                if (categoryImageFile != null)
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
                    ext = Path.GetExtension(categoryImageFile.FileName).ToLower();
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
                            categoryImageFile.CopyTo(inputStream);
                        }
                        formData.ImagePath = fileName;
                        var status = _categoryService.InsertUpdateCategory(formData);
                        return Json(new { Status = (status == 1 ? "success" : "failed"), Message = formData.Id == 0 ? "Category has been added" : "Category has been updated" });
                    }
                    else
                    {
                        return Json(new { Status = "failed", Message = "Only png, jpg, jpeg file allowed." });
                    }
                }
                else
                {
                    var status = _categoryService.InsertUpdateCategory(formData);
                    if (status == 0)
                    {
                        return Json(new { Status = "failed", Message = "Unable to update category" });
                    }
                    else
                        return Json(new { Status = (status == 1 ? "success" : "failed"), Message = formData.Id == 0 ? "Category has been added" : "Category has been updated" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = "failed", Message = ex.Message.ToString() });
            }
        }
    }
}
