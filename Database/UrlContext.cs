using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Database
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

        public DbSet<UrlEntry> UrlEntries { get; set; }
    }
}
