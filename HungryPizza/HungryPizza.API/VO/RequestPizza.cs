using System;
using System.Text.Json.Serialization;

namespace HungryPizza.API.VO
{
    public class RequestPizza
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
