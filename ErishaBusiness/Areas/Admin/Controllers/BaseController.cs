using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using ErishaBusiness.Core;
using ErishaBusiness.Areas.Admin.CustomAttributes;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Role.Admin)]
    public class BaseController : Controller
    {
        public Guid UserId { get { return new Guid(this.GetClaimValue("USERID")); } }
        private string GetClaimValue(string type)
        {
            var value = User.Claims.FirstOrDefault(x => x.Type == type)?.Value;
            return value ?? string.Empty;
        }

    }
}
