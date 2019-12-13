using System;

namespace eRestaurant.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? WaiterId { get; set; }
        public Waiter Waiter { get; set; }
    }
}
