using UrlShorter.Date.Base;
using UrlShorter.Models;

namespace UrlShorter.Date.Services
{
    public class UserService : EntityBaseRepository<UserTable>, IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context) : base(context) { }
    }
}
