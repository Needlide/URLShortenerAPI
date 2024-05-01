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

        [ForeignKey("User")]
        public int UserId { get; set; } // created by

        [Required]
        public DateTime CreatedDate { get; } = DateTime.UtcNow;
    }
}
