using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryPizza 
    {
        Task<IEnumerable<Pizza>> GetAll();

        Task<IEnumerable<Pizza>> GetList(params Guid[] ids);
    }
}
