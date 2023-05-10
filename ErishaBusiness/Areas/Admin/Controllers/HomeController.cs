using Microsoft.AspNetCore.Mvc;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
