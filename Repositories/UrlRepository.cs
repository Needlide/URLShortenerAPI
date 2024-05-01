using URLShortenerAPI.Abstract;
using URLShortenerAPI.Database;
using URLShortenerAPI.Models;

namespace URLShortenerAPI.Repositories
{
    public class UrlRepository(UrlContext context) : IUrlRepository
    {
        public void Add(UrlEntry entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(UrlEntry entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<UrlEntry> GetAll()
        {
            return context.Set<UrlEntry>().ToList();
        }

        public UrlEntry? GetById(int id)
        {
            return context.Set<UrlEntry>().Find(id);
        }

        public UrlEntry GetByOriginalUrl(string originalUrl)
        {
            return context.Set<UrlEntry>().First(x => x.OriginalUrl == originalUrl);
        }

        public void Update(UrlEntry entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
