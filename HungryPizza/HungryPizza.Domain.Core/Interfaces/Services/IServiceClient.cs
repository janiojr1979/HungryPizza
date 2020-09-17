using HungryPizza.Domain.Models;
using System;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Core.Interfaces.Services
{
    public interface IServiceClient
    {
        Task<bool> Add(Client client);

        Task<bool> Delete(Guid id);

        Task<bool> Update(Client client);

        Task<Client> Get(string email);

        Task<Client> Get(Guid id);
    }
}
