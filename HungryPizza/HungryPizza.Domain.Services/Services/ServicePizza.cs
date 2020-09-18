using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Services.Services
{
    public class ServicePizza : IServicePizza
    {
        private readonly IRepositoryPizza _repositoryPizza;
        public ServicePizza(IRepositoryPizza repositoryPizzat)
        {
            _repositoryPizza = repositoryPizzat;
        }

        public async Task<bool> Add(Pizza pizza)
        {
            return await _repositoryPizza.Add(pizza);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repositoryPizza.Delete(id);
        }

        public async Task<IEnumerable<Pizza>> GetAll()
        {
            return await _repositoryPizza.GetAll();
        }

        public async Task<Pizza> Get(Guid id)
        {
            return await _repositoryPizza.Get(id);
        }

        public async Task<bool> Update(Pizza pizza)
        {
            return await _repositoryPizza.Update(pizza);
        }
    }
}
