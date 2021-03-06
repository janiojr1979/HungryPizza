﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HungryPizza.API.VO
{
    public class RequestOrder
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Guid? ClientId { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public IEnumerable<RequestOrderItem> Items { get; set; }
    }
}
