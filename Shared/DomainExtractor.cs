using System.Text.RegularExpressions;

namespace URLShortenerAPI.Shared
{
    public static class DomainExtractor
    {
        public static string ExtractDomain(string url)
        {
            string pattern = @"^(?:https?:\/\/)?(?:[^@\n]+@)?(?:www\.)?([^:\/\n]+)";
            Match match = Regex.Match(url, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                throw new ArgumentException("Invalid URL format");
            }
        }
    }
}
