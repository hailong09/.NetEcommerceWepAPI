using backend.Models;

namespace backend.Repositories
{
    public interface IOrderRepository
    {
        public Task<OrderModels> GetOrder(string id);
        public Task CreateOrder(OrderModels order);
        public Task<IEnumerable<OrderModels>> GetOrders(string userId);
    }
}