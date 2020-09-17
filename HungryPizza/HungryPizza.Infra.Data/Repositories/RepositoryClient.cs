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

        public async Task<bool> Add(Client client)
        {
            var query = @"INSERT INTO CLIENT(ID, NAME, ADDRESS, ZIPCODE, STATE, CITY, EMAIL, PHONE)
                          VALUES(@ID, @NAME, @ADDRESS, @ZIPCODE, @STATE, @CITY, @EMAIL, @PHONE)";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { ID = client.Id, NAME = client.Name, ADDRESS = client.Address, ZIPCODE = client.ZipCode, STATE = client.State, CITY = client.City, EMAIL = client.Email, PHONE = client.Phone })) > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var query = @"DELETE FROM CLIENT WHERE ID = {=id}";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { id })) > 0;
        }

        public async Task<Client> Get(string email)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryFirstOrDefaultAsync<Client>("SELECT TOP 1 * FROM CLIENT (NOLOCK) WHERE EMAIL = {=email}", new { email });
        }

        public async Task<Client> Get(Guid id)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryFirstOrDefaultAsync<Client>("SELECT TOP 1 * FROM CLIENT (NOLOCK) WHERE ID = {=id}", new { id });
        }

        public async Task<bool> Update(Client client)
        {
            var query = @"UPDATE CLIENT SET NAME = @NAME, ADDRESS = @ADDRESS, ZIPCODE = @ZIPCODE, STATE = @STATE, CITY = @CITY, EMAIL = @EMAIL, PHONE = @PHONE WHERE ID = @ID";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { ID = client.Id, NAME = client.Name, ADDRESS = client.Address, ZIPCODE = client.ZipCode, STATE = client.State, CITY = client.City, EMAIL = client.Email, PHONE = client.Phone })) > 0;
        }
    }
}
