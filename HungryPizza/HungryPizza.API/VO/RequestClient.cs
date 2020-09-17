using System;
using System.Text.Json.Serialization;

namespace HungryPizza.API.VO
{
    public class RequestClient
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
