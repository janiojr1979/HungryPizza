using Dapper;
using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Models;
using HungryPizza.Infra.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HungryPizza.Infra.Data.Repositories
{
    public class RepositoryPizza : IRepositoryPizza
    {
        private readonly ConnectionStrings _connectionStrings;

        public RepositoryPizza(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<IEnumerable<Pizza>> GetAll()
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryAsync<Pizza>("SELECT * FROM PIZZA (NOLOCK)");
        }

        public async Task<IEnumerable<Pizza>> GetList(params Guid[] ids)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryAsync<Pizza>("SELECT * FROM PIZZA (NOLOCK) WHERE ID IN @IDS", new { IDS = ids });
        }
    }
}
