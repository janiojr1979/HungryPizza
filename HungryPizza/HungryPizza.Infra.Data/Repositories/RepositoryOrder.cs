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

        public async Task<Order> Get(Guid id)
        {
            var query = @"SELECT TOP 1 * FROM ORDER (NOLOCK) WHERE ID = {=id} ORDER BY 1
                          SELECT * FROM ORDERITEM (NOLOCK) WHERE ORDERID = {=id}";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            using var multi = await connection.QueryMultipleAsync(query, new { id });
            var order = (await multi.ReadAsync<Order>()).Single();

            order.Items = await multi.ReadAsync<OrderItem>();

            //var orderTask = connection.QueryFirstOrDefaultAsync<Order>(query, new { id });
            //var itemsTask = GetItems(id);
            //var order = await orderTask;
            //order.Items = await itemsTask;

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllByClient(Guid clientId)
        {
            var query = @"SELECT TOP 1 * FROM ORDER (NOLOCK) WHERE CLIENTID = {=clientId} ORDER BY 1
                          SELECT * FROM ORDERITEM (NOLOCK) OI
                          INNER JOIN ORDER (NOLOCK) O ON OI.ORDERID = O.ID
                          WHERE O.CLIENTID = {=clientId}";
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            using var multi = await connection.QueryMultipleAsync(query, new { clientId });
            var orders = await multi.ReadAsync<Order>();
            var items = await multi.ReadAsync<OrderItem>();

            orders.ForEach(o => o.Items = items.Where(i => i.OrderId == o.Id));

            return orders;
        }

        private async Task<IEnumerable<OrderItem>> GetItems(Guid orderId)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryAsync<OrderItem>("SELECT * FROM ORDERITEM (NOLOCK) WHERE ORDERID = {=orderId}", new { orderId });
        }

        private async Task<IEnumerable<OrderItem>> GetItems(Guid[] orderIds)
        {
            using var connection = new SqlConnection(_connectionStrings.HungryPizzaDB);
            return await connection.QueryAsync<OrderItem>("SELECT * FROM ORDERITEM (NOLOCK) WHERE ORDERID IN @IDs", new { IDs = orderIds });
        }
    }
}
