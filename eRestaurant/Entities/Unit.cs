﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eRestaurant.Entities
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Dish> Dishes { get; set; }
    }
}