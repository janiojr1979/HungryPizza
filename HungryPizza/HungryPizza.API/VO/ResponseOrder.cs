using System;
using System.Collections.Generic;

namespace HungryPizza.API.VO
{
    public class ResponseOrder
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<ResponseOrderItem> Items { get; set; }
    }
}
