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
    public class CmsPageController : BaseController
    {
        readonly ICmsPageService _cmsPageService;

        public CmsPageController(ICmsPageService cmsPageService)
        {
            _cmsPageService = cmsPageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadCmsPageData()
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

                var CmsPageData = await _cmsPageService.GetAll(0, skip, pageSize, sortColumn, sortColumnDirection, searchValue);
                var CmsPages = CmsPageData.CmsPages.ToList();

                recordsTotal = CmsPageData.RowCounts;
                var data = CmsPages;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCmsPageById(int Id)
        {
            try
            {
                var CmsPageData = await _cmsPageService.DeleteDetailById(Id);
                return Json(new { status = 1, data = "CmsPage has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, data = "Unable to delete CmsPage." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = new CmsPageDto();
            if (Id > 0)
            {
                result = await _cmsPageService.GetDetailByID(Id);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(CmsPageDto objCmsPage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objCmsPage.Url = string.Join("-", objCmsPage.Name.Split(" ")).ToLower();
                    var status = _cmsPageService.InsertUpdateDetail(objCmsPage);
                    if (status == 0)
                        return View(objCmsPage);
                    else
                        return LocalRedirect("~/admin/cmspage");
                }
                else
                    return View(objCmsPage);
            }
            catch (Exception ex)
            {
                return View(objCmsPage);
            }
        }
    }
}
