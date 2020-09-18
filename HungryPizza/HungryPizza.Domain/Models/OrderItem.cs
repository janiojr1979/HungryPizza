using System;

namespace HungryPizza.Domain.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid PizzaId1 { get; set; }

        public Guid? PizzaId2 { get; set; }

        public string PizzaName1 { get; set; }

        public string PizzaName2 { get; set; }

        public decimal Price { get; set; }
    }
}
