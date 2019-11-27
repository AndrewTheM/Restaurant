using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRestaurant.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? StatusId { get; set; }
        public OrderStatus Status { get; set; }

        public int? WaiterId { get; set; }
        public Waiter Waiter { get; set; }

        public int TableNbr { get; set; }
        public bool Tips { get; set; }
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime? Date { get; set; }

        public IList<OrderDish> OrderDishes { get; set; }
    }
}
