using Microsoft.AspNetCore.Identity;

namespace eRestaurant.Entities
{
    public class User : IdentityUser
    {
        public UserProfile Profile { get; set; }
    }
}
