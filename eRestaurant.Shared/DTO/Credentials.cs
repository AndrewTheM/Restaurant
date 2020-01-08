using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Shared.DTO
{
    public class Credentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
