using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Entities
{
    public class User: IdentityUser
    {
        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
