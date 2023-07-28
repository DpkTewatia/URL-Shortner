using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UrlShorter.Models;

namespace UrlShorter.Date.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {  
            _context = context; 
        }
        private string GenerateShortUrl(string originalUrl)
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

           
            string shortUrl = new string(Enumerable.Repeat(characters, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"https://myshorturl.com/{shortUrl}";
        }
        public void AddUrl(UrlTable url)
        {
            url.ShortUrl = GenerateShortUrl(url.OriginalUrl);
            _context.UrlsTabl.Add(url);
            _context.SaveChanges();
        }

        public void DeleteUrl(int urlId)
        {
            var url = _context.UrlsTabl.Find(urlId);
            if (url != null)
            {
                _context.UrlsTabl.Remove(url);
                _context.SaveChanges();
            }
        }

        public List<UrlTable> GetUrls()
        {
            return _context.UrlsTabl.ToList();
        }

        public UrlTable GetByUrlId(int urlId)
        {
            return _context.UrlsTabl.FirstOrDefault(u => u.Id == urlId);
        }
    }
}

