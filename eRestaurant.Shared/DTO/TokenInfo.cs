using System;

namespace eRestaurant.Shared.DTO
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
