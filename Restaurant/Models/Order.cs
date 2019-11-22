﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public User Customer { get; set; }

        public int? StatusId { get; set; }
        public OrderStatus Status { get; set; }

        [NotMapped]
        public int? WaiterId { get; set; }
        [NotMapped]
        public User Waiter { get; set; }

        public int TableNbr { get; set; }
        public bool Tips { get; set; }
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime? Date { get; set; }
    }
}
