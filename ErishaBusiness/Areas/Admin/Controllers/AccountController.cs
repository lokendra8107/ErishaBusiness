using ErishaBusiness.Core;
using ErishaBusiness.Data.DTOS;
using ErishaBusiness.Repo;
using ErishaBusiness.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace ErishaBusiness.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index(string returnUrl)
        {
            LoginDetailDto objLogin = new LoginDetailDto();
            objLogin.ReturnUrl = returnUrl;
            objLogin.RememberMe = false;
            var cookie = Request.Cookies["LoginUserName"];
            if (HttpContext.Session.GetString("LoginUserName") != null)
            {
                objLogin.RememberMe = true;
                objLogin.Email = HttpContext.Session.GetString("LoginUserName");
                objLogin.Password = HttpContext.Session.GetString("LoginUserPassword");
            }
            return View(objLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDetailDto objLogin)
        {
            if (ModelState.IsValid)
            {
                UserLoginDto user = await _accountRepository.GetUserLoginDetailByEmail(objLogin.Email);
                if(objLogin.RememberMe == true)
                {
                    HttpContext.Session.SetString("LoginUserName", objLogin.Email);
                    HttpContext.Session.SetString("LoginUserPassword", objLogin.Password);
                }
                if (user == null)
                {
                    objLogin.ErrorMessage = "UserName does not exist";
                    return View(objLogin);
                }
                else if (!PasswordEncryption.PasswordsMatch(user.Password, objLogin.Password, user.SaltKey))
                {
                    objLogin.ErrorMessage = "Invalid username or password.";
                    return View(objLogin);
                }
                else if (user.RoleId != (int)RoleType.Admin)
                {
                    objLogin.ErrorMessage = "User is not valid";
                    return View(objLogin);
                }
                else if (!user.IsActive)
                {
                    objLogin.ErrorMessage = "User is inactive now";
                    return View(objLogin);
                }
                else
                {
                    LoginDetails nuser = new LoginDetails
                    {
                        UserName = user.Name,
                        UserId = user.Id,
                        UserEmail = user.Email,
                        RoleName = user.Title,
                        ACCESS_LEVEL = user.Title,
                        Logo = "",
                        id = user.Id.ToString(),
                    };
                    var key = Encoding.ASCII.GetBytes(SiteKeys.Token);
                    var JWToken = new JwtSecurityToken(
                        issuer: SiteKeys.Domain,
                        audience: SiteKeys.Domain,
                        claims: GetUserClaims(nuser),
                        notBefore: new DateTimeOffset(DateTime.UtcNow).DateTime,
                        expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                    HttpContext.Session.SetString("JWToken", token);
                    if (!string.IsNullOrEmpty(objLogin.ReturnUrl))
                        return LocalRedirect(objLogin.ReturnUrl);
                    else
                        return LocalRedirect("~/Admin/Home");
                }
            }
            else
            {
                return View(objLogin);
            }
        }

        #region[START : LOGOUT]
        [HttpGet]
        public IActionResult LogOff()
        {
            RemoveAuthentication();
            return LocalRedirect("~/Admin");
        }
        #endregion

        #region[START : lOGOUT USER]
        public void RemoveAuthentication()
        {
            HttpContext.Session.Remove("JWToken");
        }
        #endregion

        private IEnumerable<Claim> GetUserClaims(LoginDetails user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(ClaimTypes.Role, user.ACCESS_LEVEL);
            claims.Add(_claim);
            _claim = new Claim(ClaimTypes.Email, user.UserEmail);
            claims.Add(_claim);
            _claim = new Claim(ClaimTypes.Name, user.UserName);
            claims.Add(_claim);
            _claim = new Claim("USERID", user.UserId.ToString());
            claims.Add(_claim);
            _claim = new Claim("EMAILID", user.UserEmail);
            claims.Add(_claim);
            _claim = new Claim("USERNAME", user.UserName);
            claims.Add(_claim);
            _claim = new Claim(user.ACCESS_LEVEL, user.ACCESS_LEVEL);
            claims.Add(_claim);
            return claims.AsEnumerable<Claim>();
        }
    }
}
