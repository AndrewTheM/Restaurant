using System;

namespace eRestaurant.API.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Customer Customer { get; set; }

        public Waiter Waiter { get; set; }
    }
}
