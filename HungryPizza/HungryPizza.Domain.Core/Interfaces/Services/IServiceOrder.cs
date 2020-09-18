using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Core.Interfaces.Services
{
    public interface IServiceOrder
    {
        Task<Order> Get(Guid id);

        Task<IEnumerable<Order>> GetAllByClient(Guid clientId);

        Task<bool> Add(Order order);

        Task<bool> Update(Order order);

        Task<bool> Delete(Guid id);
    }
}
