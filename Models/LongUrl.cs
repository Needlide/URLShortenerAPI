using System.ComponentModel.DataAnnotations;

namespace URLShortenerAPI.Models
{
    public class LongUrl
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
