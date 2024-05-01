using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Database
{
    public class UrlContext : DbContext
    {
        public DbSet<UrlEntry> UrlEntries { get; set; }
    }
}
