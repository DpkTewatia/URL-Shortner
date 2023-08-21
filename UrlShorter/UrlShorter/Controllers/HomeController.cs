using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using UrlShorter.Date.Services;
using UrlShorter.Models;

namespace UrlShorter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;
        public HomeController(IUserService service)
        {
            _service = service;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string txtUserName, string txtPassword)
        {
            var user = _service.GetByUserName(txtUserName);
           

            if (user != null)
            {
                if (user.UserName == txtUserName && user.Password == txtPassword)
                {


                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Role,  user.RoleId.ToString()), 
                    new Claim(ClaimTypes.UserData, user.UserId.ToString())
                    };
                    var identity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    return RedirectToAction("Index", "TableURL");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
    
