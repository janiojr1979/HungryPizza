using HungryPizza.Domain.Models;
using System;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryClient
    {
        Task<Client> Get(string email);

        Task<Client> Get(Guid id);

        Task<bool> Add(Client client);

        Task<bool> Update(Client client);

        Task<bool> Delete(Guid id);
    }
}
