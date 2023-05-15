﻿using ErishaBusiness.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace ErishaBusiness.Areas.Admin.CustomAttributes
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUrl = context.HttpContext.Request.Path;
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                bool flagAgreement = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(item, item) && Role.Admin == item)
                        flagClaim = true;
                }
                if (!flagClaim)
                {
                    if (context.HttpContext.Request.IsAjaxRequest())
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized; //Set HTTP 401 
                    else
                    {
                        context.Result = new RedirectResult("~/admin?returnUrl="+ currentUrl);
                    }
                }
            }
            else
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden; //Set HTTP 403 - 
                }
                else
                {
                    context.Result = new RedirectResult("~/admin?returnUrl="+ currentUrl);
                }
            }
            return;
        }
    }
}
