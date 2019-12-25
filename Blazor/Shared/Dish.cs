using System;
using System.Collections.Generic;

namespace Blazor
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double PortionSize { get; set; }
        public TimeSpan? CookingTime { get; set; }
        public int? TypeId { get; set; }
        public int? UnitId { get; set; }
    }
}
