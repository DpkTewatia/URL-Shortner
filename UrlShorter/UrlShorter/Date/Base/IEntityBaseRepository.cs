using System.Collections.Generic;
using UrlShorter.Models;

namespace UrlShorter.Date.Base
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        List<UrlTable> GetUrls();
        public void AddUrl(UrlTable url);
        public void DeleteUrl(int urlId);
        public UrlTable GetByUrlId(int urlId); 
        // public string ShortenUrl(string originalUrl);
    }
}
