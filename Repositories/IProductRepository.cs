using backend.Models;

namespace backend.Repositories
{
    public interface IProductRepository
    {

        public Task<Product> GetProduct(string id);
    
        public Task<IEnumerable<Product>> GetProducts();
        public Task CreateProduct(Product product);

        public Task UpdateProduct(Product product);




    }
}