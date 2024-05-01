using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Database
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
