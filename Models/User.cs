using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using URLShortenerAPI.Shared;

namespace URLShortenerAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Role Role { get; set; } = Role.Unregistered;

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UrlEntry> UrlEntries { get; set; }
    }
}
