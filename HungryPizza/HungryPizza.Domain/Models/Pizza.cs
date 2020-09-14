using System;

namespace HungryPizza.Domain.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
