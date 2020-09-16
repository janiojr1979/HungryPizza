using Dapper;
using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Models;
using HungryPizza.Infra.Data.Common;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HungryPizza.Infra.Data.Repositories
{
    public class RepositoryClient : IRepositoryClient
    {
        private readonly ConnectionStrings _connectionStrings;

        public RepositoryClient(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<Client> GetClient(string email)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryFirstOrDefaultAsync<Client>("SELECT TOP 1 * FROM CLIENT (NOLOCK) WHERE EMAIL = {=email}", new { email });
        }

        public async Task<Client> GetClient(Guid id)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryFirstOrDefaultAsync<Client>("SELECT TOP 1 * FROM CLIENT (NOLOCK) WHERE ID = {=id}", new { id });
        }
    }
}
