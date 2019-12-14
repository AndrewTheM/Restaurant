using Microsoft.AspNetCore.Identity;

namespace eRestaurant.Entities
{
    public class User : IdentityUser
    {
        public int? ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}
