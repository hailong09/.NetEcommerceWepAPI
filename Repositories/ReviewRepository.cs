using System.Security.Cryptography.X509Certificates;
using backend.Models;
using MongoDB.Driver;

namespace backend.Repositories
{

    public class ReviewRepository : IReviewRepository
    {

        private const string databaseName = "Shopping";
        private const string collectionName = "reviews";

        private readonly IMongoCollection<Review> reviewsCollection;

        private readonly FilterDefinitionBuilder<Review> filterDefinitionBuilder = Builders<Review>.Filter;

        public ReviewRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            reviewsCollection = database.GetCollection<Review>(collectionName);
        }

        public async Task CreateReview(Review review)
        {
            await reviewsCollection.InsertOneAsync(review);
        }

        public async Task DeleteReview(string id)
        {
            var filter = filterDefinitionBuilder.Eq(existingReview => existingReview.Id, id);
            await reviewsCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateReview(Review review)
        {
            var filter = filterDefinitionBuilder.Eq(existingReview => existingReview.Id, review.Id);
            await reviewsCollection.ReplaceOneAsync(filter, review);
        }


        public async Task<Review> GetReview(string id)
        {
            var filter = filterDefinitionBuilder.Eq(review => review.Id, id);
            return await reviewsCollection.Find(filter).FirstOrDefaultAsync();
        }

        


    }
}