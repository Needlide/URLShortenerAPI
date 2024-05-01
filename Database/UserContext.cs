using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Database
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
