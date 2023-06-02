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
    public class SiteSettingController : BaseController
    {
        readonly ISiteSettingService _siteSettingService;

        public SiteSettingController(ISiteSettingService SiteSettingService)
        {
            _siteSettingService = SiteSettingService;
        }

        public async Task<IActionResult> Index()
        {
            var result = new SiteSettingDto();
            result = await _siteSettingService.GetDetailByID();
            result.Logo = SiteKeys.AssetsDomain + result.Logo;
            result.Favicon = SiteKeys.AssetsDomain + result.Favicon;
            return View(result);
        }

        [HttpPost]
        public JsonResult SaveSiteSettingData(string data, IFormFile SiteSettingImageFile, IFormFile SiteSettingFeviconImageFile)
        {
            try
            {
                SiteSettingDto formData = JsonConvert.DeserializeObject<SiteSettingDto>(data);
                var logoImage = "";
                var feviconImage = "";
                if (formData.Id > 0)
                {
                    var SiteSettingData = _siteSettingService.GetDetailByID().Result;
                    logoImage = SiteSettingData.Logo;
                    feviconImage = SiteSettingData.Favicon;
                }

                var ext = "";
                var fileName = "";

                var feviconext = "";
                var feviconfileName = "";
                string folderPath = SiteKeys.ImageFolder;
                if (SiteSettingImageFile != null || SiteSettingFeviconImageFile != null)
                {
                    if (!string.IsNullOrEmpty(logoImage) && SiteSettingImageFile != null)
                    {
                        string folderPaths = SiteKeys.ImageFolder;
                        string filePaths = $"{folderPaths}{logoImage}";
                        if (System.IO.File.Exists(filePaths))
                        {
                            System.IO.File.Delete(filePaths);
                        }
                    }
                    if (!string.IsNullOrEmpty(feviconImage) && SiteSettingFeviconImageFile != null)
                    {
                        string folderPaths = SiteKeys.ImageFolder;
                        string filePaths = $"{folderPaths}{feviconImage}";
                        if (System.IO.File.Exists(filePaths))
                        {
                            System.IO.File.Delete(filePaths);
                        }
                    }
                    if (SiteSettingImageFile != null)
                    {
                        ext = Path.GetExtension(SiteSettingImageFile.FileName).ToLower();
                        fileName = Guid.NewGuid() + ext;
                    }
                    if (SiteSettingFeviconImageFile != null)
                    {
                        feviconext = Path.GetExtension(SiteSettingFeviconImageFile.FileName).ToLower();
                        feviconfileName = Guid.NewGuid() + feviconext;
                    }
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    if (!string.IsNullOrEmpty(fileName) && SiteSettingImageFile != null)
                    {
                        string filePath = $"{folderPath}{fileName}";
                        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                        {
                            using (var inputStream = new FileStream(filePath, FileMode.Create))
                            {
                                SiteSettingImageFile.CopyTo(inputStream);
                            }
                            formData.Logo = fileName;
                        }
                        else
                        {
                            return Json(new { Status = "failed", Message = "Only png, jpg, jpeg file allowed." });
                        }
                    }
                    if (!string.IsNullOrEmpty(feviconfileName) && SiteSettingFeviconImageFile != null)
                    {
                        string filePath = $"{folderPath}{feviconfileName}";
                        if (feviconext == ".png" || feviconext == ".jpg" || feviconext == ".jpeg")
                        {
                            using (var inputStream = new FileStream(filePath, FileMode.Create))
                            {
                                SiteSettingFeviconImageFile.CopyTo(inputStream);
                            }
                            formData.Favicon = feviconfileName;
                        }
                        else
                        {
                            return Json(new { Status = "failed", Message = "Only png, jpg, jpeg file allowed." });
                        }
                    }
                    var status = _siteSettingService.InsertUpdateDetail(formData);
                    return Json(new { Status = (status == 1 ? "success" : "failed"), Message = formData.Id == 0 ? "Site Setting has been added" : "SiteSetting has been updated" });
                }
                else
                {
                    var status = _siteSettingService.InsertUpdateDetail(formData);
                    if (status == 0)
                    {
                        return Json(new { Status = "failed", Message = "Unable to update Site Setting" });
                    }
                    else
                        return Json(new { Status = (status == 1 ? "success" : "failed"), Message = formData.Id == 0 ? "Site Setting has been added" : "Site Setting has been updated" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = "failed", Message = ex.Message.ToString() });
            }
        }
    }
}
