using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryOrder 
    {
        Task<Order> Get(Guid id);

        Task<IEnumerable<Order>> GetAllByClient(Guid clientId);
    }
}
