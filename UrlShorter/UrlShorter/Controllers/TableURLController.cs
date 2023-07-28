using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using UrlShorter.Date.Services;
using UrlShorter.Models;

namespace UrlShorter.Controllers
{
    [Authorize]
    public class TableURLController : Controller
    {
        private readonly IUrlsService _service;
        public TableURLController(IUrlsService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var date = _service.GetUrls();
            return View(date);
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
        public IActionResult AddUrl(UrlTable url)
        {
            _service.AddUrl(url);
            return Ok("Url add");
        }

        [HttpGet]
        
        public IActionResult Delete(int urlId)
        {
            var url = _service.GetByUrlId(urlId);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int urlId)
        {
            _service.DeleteUrl(urlId);
            return RedirectToAction("Index");
        }
    }
}
