using System;
using backend.Models;
using MongoDB.Driver;

namespace backend.Repositories
{
    class UserRepository : IUserRepository
    {
        private const string databaseName = "Shopping";
        private const string collectionName = "users";
        private readonly IMongoCollection<User> usersCollection;

        private readonly FilterDefinitionBuilder<User> filterDefinitionBuilder = Builders<User>.Filter;

        public UserRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            usersCollection = database.GetCollection<User>(collectionName);
        }

        public async Task Register(User user)
        {

            await usersCollection.InsertOneAsync(user);

        }

        public async Task<User> GetUser(string email)
        {
            var filter = filterDefinitionBuilder.Eq(user => user.Email, email);
            return await usersCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}