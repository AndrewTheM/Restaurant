using Microsoft.AspNetCore.Identity;

namespace eRestaurant.API.Entities
{
    public class User : IdentityUser
    {
        public UserProfile Profile { get; set; }
    }
}
