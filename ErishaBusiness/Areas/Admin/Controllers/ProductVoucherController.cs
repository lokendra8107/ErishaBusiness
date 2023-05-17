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
    public class ProductVoucherController : BaseController
    {
        readonly IVoucherService _productVoucherService;

        public ProductVoucherController(IVoucherService productVoucherService)
        {
            _productVoucherService = productVoucherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadProductVoucherData()
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

                var ProductVoucherData = await _productVoucherService.GetAllProductVoucher(0, skip, pageSize, sortColumn, sortColumnDirection, searchValue);
                var categories = ProductVoucherData.ProductVouchers.ToList();

                recordsTotal = ProductVoucherData.RowCounts;
                var data = categories;
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductVoucherById(int Id)
        {
            try
            {
                var getProductVoucherData = _productVoucherService.GetProductVoucherByID(Id).Result;

                var ProductVoucherData = await _productVoucherService.DeleteProductVoucherById(Id);
                return Json(new { status = 1, data = "ProductVoucher has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, data = "Unable to delete ProductVoucher." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = new ProductVoucherDto();
            result.StartDate = DateTime.Now;
            result.EndDate = DateTime.Now;
            if (Id > 0)
            {
                result = await _productVoucherService.GetProductVoucherByID(Id);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(ProductVoucherDto objData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var status = _productVoucherService.InsertUpdateProductVoucher(objData);
                    if (status == 0)
                        return View(objData);
                    else
                        return LocalRedirect("~/admin/productvoucher");
                }
                else
                    return View(objData);
            }
            catch (Exception ex)
            {
                return Json(new { Status = "failed", Message = ex.Message.ToString() });
            }
        }
    }
}
