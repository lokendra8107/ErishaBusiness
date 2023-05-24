using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    public class BannerLayoutController : BaseController
    {
        readonly IBannerLayoutService _bannerLayoutService;

        public BannerLayoutController(IBannerLayoutService bannerLayoutService)
        {
            _bannerLayoutService = bannerLayoutService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadBannerLayoutData()
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

                var BannerLayoutData = await _bannerLayoutService.GetAll(0, skip, pageSize, sortColumn, sortColumnDirection, searchValue);
                var BannerLayouts = BannerLayoutData.BannerLayouts.ToList();
                BannerLayouts.ForEach(x => x.ImageUrl = (SiteKeys.AssetsDomain + x.ImageUrl));

                recordsTotal = BannerLayoutData.RowCounts;
                var data = BannerLayouts;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBannerLayoutById(int Id)
        {
            try
            {
                var getBannerLayoutData = _bannerLayoutService.GetDetailByID(Id).Result;
                if (!string.IsNullOrEmpty(getBannerLayoutData.ImageUrl))
                {
                    string folderPaths = SiteKeys.ImageFolder;
                    string filePaths = $"{folderPaths}{getBannerLayoutData.ImageUrl}";
                    if (System.IO.File.Exists(filePaths))
                    {
                        System.IO.File.Delete(filePaths);
                    }
                }

                var BannerLayoutData = await _bannerLayoutService.DeleteDetailById(Id);
                return Json(new { status = 1, data = "BannerLayout has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, data = "Unable to delete BannerLayout." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = new BannerLayoutDto();
            if (Id > 0)
            {
                result = await _bannerLayoutService.GetDetailByID(Id);
                result.ImageUrl = SiteKeys.AssetsDomain + result.ImageUrl;
            }
            ViewBag.Categories = await _bannerLayoutService.GetCategories();
            return View(result);
        }

        [HttpPost]
        public JsonResult SaveBannerLayoutData(string data, IFormFile BannerLayoutImageFile)
        {
            try
            {
                BannerLayoutDto formData = JsonConvert.DeserializeObject<BannerLayoutDto>(data);
                formData.AddedBy = UserId.ToString();
                var existImage = "";
                if (formData.Id > 0)
                {
                    var BannerLayoutData = _bannerLayoutService.GetDetailByID(formData.Id).Result;
                    existImage = BannerLayoutData.ImageUrl;
                }

                var ext = "";
                var fileName = "";
                if (BannerLayoutImageFile != null)
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
                    ext = Path.GetExtension(BannerLayoutImageFile.FileName).ToLower();
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
                            BannerLayoutImageFile.CopyTo(inputStream);
                        }
                        formData.ImageUrl = fileName;
                        var status = _bannerLayoutService.InsertUpdateDetail(formData);
                        return Json(new { Status = (status == 1 ? "success" : "failed"), Message = formData.Id == 0 ? "BannerLayout has been added" : "BannerLayout has been updated" });
                    }
                    else
                    {
                        return Json(new { Status = "failed", Message = "Only png, jpg, jpeg file allowed." });
                    }
                }
                else
                {
                    var status = _bannerLayoutService.InsertUpdateDetail(formData);
                    if (status == 0)
                    {
                        return Json(new { Status = "failed", Message = "Unable to update BannerLayout" });
                    }
                    else
                        return Json(new { Status = (status == 1 ? "success" : "failed"), Message = formData.Id == 0 ? "BannerLayout has been added" : "BannerLayout has been updated" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = "failed", Message = ex.Message.ToString() });
            }
        }
    }
}
