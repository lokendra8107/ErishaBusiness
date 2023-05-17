using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
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
    public class EmailTemplateController : BaseController
    {
        readonly IEmailTemplateRepository _mailTemplateRepository;

        public EmailTemplateController(IEmailTemplateRepository mailTemplateRepository)
        {
            _mailTemplateRepository = mailTemplateRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadEmailTemplatedData()
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

                var ProductVoucherData = await _mailTemplateRepository.GetAll(0, skip, pageSize, sortColumn, sortColumnDirection, searchValue);
                var categories = ProductVoucherData.EmailTemplates.ToList();

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
        public async Task<IActionResult> Edit(int Id)
        {
            var result = new EmailTemplateDto();
            if (Id > 0)
            {
                result = await _mailTemplateRepository.GetEmailTemplateById(Id);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(EmailTemplateDto objData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objData.AddedBy = UserId;
                    var status = _mailTemplateRepository.UpdateEmailTemplate(objData);
                    if (status == 0)
                        return View(objData);
                    else
                        return LocalRedirect("~/admin/emailtemplate");
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
