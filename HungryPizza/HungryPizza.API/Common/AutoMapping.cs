using AutoMapper;
using HungryPizza.API.VO;
using HungryPizza.Domain.Models;

namespace HungryPizza.API.Common
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RequestClient, Client>();
            CreateMap<RequestPizza, Pizza>();
            CreateMap<RequestOrder, Order>();
            CreateMap<RequestOrderItem, OrderItem>();
            CreateMap<Order, ResponseOrder>();
            CreateMap<OrderItem, ResponseOrderItem>();
        }
    }
}
