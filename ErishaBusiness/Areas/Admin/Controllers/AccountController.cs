using Microsoft.AspNetCore.Mvc;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
