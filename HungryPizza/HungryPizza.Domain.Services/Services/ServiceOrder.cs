using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HungryPizza.Domain.Services.Services
{
    public class ServiceOrder : IServiceOrder
    {
        private readonly IRepositoryOrder _repositoryOrder;
        private readonly IRepositoryPizza _repositoryPizza;
        private readonly IRepositoryClient _repositoryClient;

        public ServiceOrder(IRepositoryOrder repositoryOrder, IRepositoryPizza repositoryPizza, IRepositoryClient repositoryClient)
        {
            _repositoryOrder = repositoryOrder;
            _repositoryPizza = repositoryPizza;
            _repositoryClient = repositoryClient;
        }

        public async Task<bool> Add(Order order)
        {
            await LoadItemsInfo(order);

            if (order.ClientId.HasValue)
            {
               await LoadClientAddress(order);

                return await _repositoryOrder.Add(order);
            }

            return await _repositoryOrder.AddNoRegister(order);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repositoryOrder.Delete(id);
        }

        public async Task<Order> Get(Guid id)
        {
            return await _repositoryOrder.Get(id);
        }

        public async Task<IEnumerable<Order>> GetAllByClient(Guid clientId)
        {
            return await _repositoryOrder.GetAllByClient(clientId);
        }

        public async Task<bool> Update(Order order)
        {
            if (await _repositoryOrder.Delete(order.Id))
            {
                return await Add(order);
            }

            return false;
        }

        private async Task LoadClientAddress(Order order)
        {
            var client = await _repositoryClient.Get(order.ClientId.Value);

            order.Address = client.Address;
            order.City = client.City;
            order.ClientName = client.Name;
            order.Email = client.Email;
            order.State = client.State;
            order.ZipCode = client.ZipCode;
        }

        private async Task LoadItemsInfo(Order order)
        {
            var lstPizzas = await _repositoryPizza.GetAll();

            foreach (var item in order.Items)
            {
                var pizza1 = lstPizzas.FirstOrDefault(p => p.Id == item.PizzaId1);
                var pizza2 = item.PizzaId2 != null ? lstPizzas.FirstOrDefault(p => p.Id == item.PizzaId2) : null;

                if (pizza1 == null)
                {
                    throw new Exception($"Id de pizza inexistente. Id: {item.PizzaId1}");
                }

                if (item.PizzaId2 != null && pizza2 == null)
                {
                    throw new Exception($"Id de pizza inexistente. Id: {item.PizzaId2}");
                }

                item.PizzaName1 = pizza1.Name;

                if (pizza2 == null)
                {
                    item.Price = pizza1.Price;                    
                }
                else
                {
                    item.Price = (pizza1.Price + pizza2.Price) / 2m;
                    item.PizzaName2 = pizza2.Name;
                }
            }
        }
    }
}
