using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace eRestaurant.Entities
{
    public class User: IdentityUser
    {
        public int? ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}
