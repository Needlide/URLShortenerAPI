using URLShortenerAPI.Models;

namespace URLShortenerAPI.Abstract
{
    public interface IUrlRepository : IRepository<UrlEntry>
    {
        UrlEntry GetByOriginalUrl(string originalUrl);
    }
}
