using System;

namespace Blazor
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double PortionSize { get; set; }
        public double? CookingTime { get; set; }
        public string TypeId { get; set; }
        public string UnitId { get; set; }
    }
}
