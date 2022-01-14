using backend.Controllers;
using backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private const string databaseName = "Shopping";
        private const string collectionName = "orders";
        private readonly IMongoCollection<OrderModels> ordersCollection;
        private readonly FilterDefinitionBuilder<OrderModels> filterDefinitionBuilder = Builders<OrderModels>.Filter;



        public OrderRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            ordersCollection = database.GetCollection<OrderModels>(collectionName);

        }

        public async Task<OrderModels> GetOrder(string id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            return await ordersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateOrder(OrderModels order)
        {
            await ordersCollection.InsertOneAsync(order);
        }

        public async Task<IEnumerable<OrderModels>> GetOrders(string userId)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.UserId, userId);
            return await ordersCollection.Find(filter).ToListAsync();
        }
    }
}