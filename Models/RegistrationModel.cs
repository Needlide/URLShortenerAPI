using System.ComponentModel.DataAnnotations;
using URLShortenerAPI.Shared;

namespace URLShortenerAPI.Models
{
    public class RegistrationModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
