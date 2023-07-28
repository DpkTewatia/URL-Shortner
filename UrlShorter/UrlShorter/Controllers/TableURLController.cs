using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UrlShorter.Date.Services;
using UrlShorter.Enums;
using UrlShorter.Models;
using UrlShorter.Models.ViewModels;

namespace UrlShorter.Controllers
{
   
    public class TableURLController : Controller
    {
        private readonly IUrlsService _service;
        public TableURLController(IUrlsService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            RoleIdEnum role = RoleIdEnum.User;
            var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (userRole != null)
            {
                Enum.TryParse(userRole, out RoleIdEnum roleId);
                role = roleId;

                
            } 
            int? userId = null;
            var claims = HttpContext.User.FindFirstValue(ClaimTypes.UserData);
            if(claims != null)
            {
                userId = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
            }
            var entities = _service.GetUrls();
            var viewModel = entities.Select(x => new UrlTableViewModel() {
              Id = x.Id,
             
              CreationDate = x.CreationDate,
              OriginalUrl = x.OriginalUrl,
              ShortUrl = x.ShortUrl,
              UserId = x.UserId
            }).ToList();
            foreach(var model in viewModel) 
            {
                if(userId != null)
                {
                    if(role == RoleIdEnum.Admin)
                    {
                        model.AllowDelete = true;
                       
                    }
                    else
                    {
                        model.AllowDelete = userId == model.UserId ? true : false;
                    }

                    
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public List<UrlTable> GetUrls()
        {
            return _service.GetUrls();
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]

        public IActionResult AddUrl(UrlTable requestUrl)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.UserData);
            if (userId != null)
            {
                requestUrl.UserId = Int32.Parse(userId);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            requestUrl.CreationDate = DateTime.Now;
            _service.AddUrl(requestUrl);
            return RedirectToAction("Index");
        }

        [HttpGet]
        
        public IActionResult Delete(int urlId)
        {
            var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (userRole != null)
            {
                Enum.TryParse(userRole, out RoleIdEnum roleId );

                if (roleId == RoleIdEnum.Admin)
                {
                    _service.DeleteUrl(urlId);
                }
                else
                {
                    var url = _service.GetByUrlId(urlId);
                    if (url != null)
                    {
                       var userId = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.UserData));
                        if(url.UserId == userId)
                        {
                            _service.DeleteUrl(urlId);
                        }

                    }
                    else
                    {
                        return NotFound();
                    }
                }
               
               
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int urlId)
        {
            _service.DeleteUrl(urlId);
            return RedirectToAction("Index");
        }
    }
}
