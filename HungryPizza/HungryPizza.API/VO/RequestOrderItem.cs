using System;
using System.Text.Json.Serialization;

namespace HungryPizza.API.VO
{
    public class RequestOrderItem
    {
        [JsonIgnore]
        public Guid OrderId { get; set; }

        public Guid PizzaId1 { get; set; }

        public Guid? PizzaId2 { get; set; }
    }
}
