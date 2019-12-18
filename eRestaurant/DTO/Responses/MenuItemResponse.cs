using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.DTO
{
    public class MenuItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Portion { get; set; }
        public string CookingTime { get; set; }
        public string Type { get; set; }
        public double Rating { get; set; }
        public byte[] Image { get; set; }
    }
}
