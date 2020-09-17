using Dapper;
using HungryPizza.Domain.Core.Interfaces.Repositories;
using HungryPizza.Domain.Models;
using HungryPizza.Infra.CrossCutting.Tools;
using HungryPizza.Infra.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HungryPizza.Infra.Data.Repositories
{
    public class RepositoryOrder : IRepositoryOrder
    {
        private readonly ConnectionStrings _connectionStrings;

        public RepositoryOrder(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<bool> Add(Order order)
        {
            var query = @"INSERT INTO ORDER(ID, CLIENTID) VALUES(@ID, @CLIENTID)";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            using var transaction = await connection.BeginTransactionAsync();
            bool succes = false;

            try
            {
                if (await connection.ExecuteAsync(query, new { ID = order.Id, CLIENTID = order.ClientId }) > 0)
                {
                    succes = await AddItems(order.Items, connection);
                }

                if (succes)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return succes;
        }

        public async Task<bool> AddNoRegister(Order order)
        {
            var query = @"INSERT INTO ORDER(ID, CLIENTID) VALUES(@ID, @EMAIL, @ADDRESS, @ZIPCODE, @STATE, @CITY)";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            using var transaction = await connection.BeginTransactionAsync();
            bool succes = false;

            try
            {
                if (await connection.ExecuteAsync(query, new { ID = order.Id, EMAIL = order.Email, @ADDRESS = order.Address, @ZIPCODE = order.ZipCode, @STATE = order.State, @CITY = order.City }) > 0)
                {
                    succes = await AddItems(order.Items, connection);
                }

                if (succes)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return succes;
        }

        public async Task<bool> Delete(Guid id)
        {
            var query = @"DELETE FROM ORDER WHERE ID = @ID";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);

            return (await connection.ExecuteAsync(query, new { ID = id })) > 0;
        }

        public async Task<Order> Get(Guid id)
        {
            var query = @"SELECT TOP 1 * FROM ORDER (NOLOCK) WHERE ID = @ID ORDER BY DATE
                          SELECT * FROM ORDERITEM (NOLOCK) WHERE ORDERID = @ID";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            using var multi = await connection.QueryMultipleAsync(query, new { ID = id });
            var order = (await multi.ReadAsync<Order>()).Single();

            order.Items = await multi.ReadAsync<OrderItem>();

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllByClient(Guid clientId)
        {
            var query = @"SELECT TOP 1 * FROM ORDER (NOLOCK) WHERE CLIENTID = @CLIENTID ORDER BY 1
                          SELECT * FROM ORDERITEM (NOLOCK) OI
                          INNER JOIN ORDER (NOLOCK) O ON OI.ORDERID = O.ID
                          WHERE O.CLIENTID = @CLIENTID";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            using var multi = await connection.QueryMultipleAsync(query, new { CLIENTID = clientId });
            var orders = await multi.ReadAsync<Order>();
            var items = await multi.ReadAsync<OrderItem>();

            orders.ForEach(o => o.Items = items.Where(i => i.OrderId == o.Id));

            return orders;
        }

        public Task<bool> Update(Order order)
        {
            throw new NotImplementedException();
        }

        async Task<bool> AddItems(IEnumerable<OrderItem> items, SqlConnection connection)
        {
            var query = "INSERT INTO ORDERITEM(ORDERID, PIZZAID1, PIZZAID2) VALUES(@ORDERID, @PIZZAID1, @PIZZAID2)";

            return (await connection.ExecuteAsync(query, items.Select(i => new { ORDERID = i.OrderId, PIZZAID1 = i.PizzaId1, PIZZAID2 = i.PizzaId2 }))) > 0;
        }
    }
}
