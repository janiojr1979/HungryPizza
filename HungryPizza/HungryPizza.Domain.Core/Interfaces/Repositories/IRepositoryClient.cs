using HungryPizza.Domain.Models;
using System;
using System.Threading.Tasks;

namespace HungryPizza.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryClient
    {
        Task<Client> GetClient(string email);

        Task<Client> GetClient(Guid id);
    }
}
