namespace URLShortenerAPI.Models
{
    public class UrlEntryDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
    }
}
