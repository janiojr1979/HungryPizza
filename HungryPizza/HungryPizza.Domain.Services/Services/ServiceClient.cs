using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Core.Interfaces.Services;
using HungryPizza.Domain.Models;
using System;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Services.Services
{
    public class ServiceClient : IServiceClient
    {
        private readonly IRepositoryClient _repositoryClient;
        public ServiceClient(IRepositoryClient repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<bool> Add(Client client)
        {
            return await _repositoryClient.Add(client);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repositoryClient.Delete(id);
        }

        public async Task<Client> Get(string email)
        {
            return await _repositoryClient.Get(email);
        }

        public async Task<Client> Get(Guid id)
        {
            return await _repositoryClient.Get(id);
        }

        public async Task<bool> Update(Client client)
        {
            return await _repositoryClient.Update(client);
        }
    }
}
