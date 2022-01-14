using backend.Dtos;
using backend.Models;

namespace backend.Extensions
{
    public static class Extensions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreateDate = user.CreateDate

            };
        }

        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Image = product.Image,
                Category = product.Category,
                Price = product.Price,
                Brand = product.Brand,
                Rating = product.Rating,
                NumReviews = product.NumReviews,
                CountInStock = product.CountInStock,
                Description = product.Description,
                CreateDate = product.CreateDate,
                Reviews = product.Reviews,
                ReviewsLists = product.ReviewsLists

            };
        }

        public static ReviewDto AsDto(this Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                Name = review.Name,
                Comment = review.Comment,
                Rating = review.Rating,
                User = review.User
            };
        }

        public static OrderDto AsDto(this OrderModels order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CartItems = order.CartItems,
                CustomerInfo = order.CustomerInfo,
                Amount = order.Amount,
                Created = order.Created
            };
        }

    }

}