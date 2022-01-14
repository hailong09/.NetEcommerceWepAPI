using backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;


namespace backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private const string databaseName = "Shopping";
        private const string collectionName = "products";

        private readonly IMongoCollection<Product> productsCollection;

        private readonly FilterDefinitionBuilder<Product> filterDefinitionBuilder = Builders<Product>.Filter;

        public ProductRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productsCollection = database.GetCollection<Product>(collectionName);

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await productsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await productsCollection.InsertOneAsync(product);
        }

        public async Task<Product> GetProduct(string id)
        {
            try
            {
                var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
                return await productsCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error!");
            }
        }


        public async Task UpdateProduct(Product product)
        {
            var filter = filterDefinitionBuilder.Eq(existingProduct => existingProduct.Id, product.Id);
            await productsCollection.ReplaceOneAsync(filter, product);
        }


    }

}
