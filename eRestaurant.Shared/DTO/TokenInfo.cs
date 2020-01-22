using System;
using System.Collections.Generic;

namespace eRestaurant.Shared.DTO
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime Expiration { get; set; }
    }
}
