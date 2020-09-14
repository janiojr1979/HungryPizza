using System;
using System.Collections.Generic;

namespace HungryPizza.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid? ClientId { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }
    }
}
