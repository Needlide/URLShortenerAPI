using System.ComponentModel.DataAnnotations;
using URLShortenerAPI.Shared;

namespace URLShortenerAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Role Role { get; set; } = Role.Unregistered;

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
