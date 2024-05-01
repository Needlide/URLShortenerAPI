using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShortenerAPI.Models
{
    public class UrlEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public string ShortenedUrl { get; set; }
    }
}
