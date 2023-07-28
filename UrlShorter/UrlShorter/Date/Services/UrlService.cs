using UrlShorter.Date.Base;
using UrlShorter.Models;

namespace UrlShorter.Date.Services
{
    public class UrlService: EntityBaseRepository<UrlTable>, IUrlsService
    {
        private readonly AppDbContext _context;
        public UrlService(AppDbContext context) : base(context) { }
    }
}
