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

        public async Task<bool> Add(Pizza pizza)
        {
            var query = @"INSERT INTO PIZZA(ID, NAME, DESCRIPTION, PRICE)
                          VALUES(@ID, @NAME, @DESCRIPTION, @PRICE)";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { ID = pizza.Id, NAME = pizza.Name, DESCRIPTION = pizza.Description, PRICE = pizza.Price })) > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var query = @"DELETE FROM PIZZA WHERE ID = {=id}";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { id })) > 0;
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

        public async Task<bool> Update(Pizza pizza)
        {
            var query = @"UPDATE SET NAME = @NAME, DESCRIPTION = @DESCRIPTION, PRICE = @PRICE WHERE ID = @ID";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { ID = pizza.Id, NAME = pizza.Name, DESCRIPTION = pizza.Description, PRICE = pizza.Price })) > 0;
        }
    }
}
