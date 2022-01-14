using backend.Models;

namespace backend.Repositories
{
    public interface IReviewRepository
    {
        public Task CreateReview(Review review);
        public Task DeleteReview(string id);
        public Task UpdateReview(Review review);
        public Task<Review> GetReview(string id);
    }
}