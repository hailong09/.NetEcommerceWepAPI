using System.Linq;
using System.Security.Claims;
using backend.Dtos;
using backend.Extensions;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository repository;
        private readonly IReviewRepository reviewRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;
        public ProductController(
            IProductRepository repository,
            IReviewRepository reviewRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            this.repository = repository;
            this.reviewRepository = reviewRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;

        }
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            if (createProductDto is null)
            {
                return BadRequest();
            }

            Product newProduct = new()
            {
                Name = createProductDto.Name,
                Image = createProductDto.Image,
                Category = createProductDto.Category,
                Price = createProductDto.Price,
                Brand = createProductDto.Brand,
                Rating = createProductDto.Rating,
                NumReviews = createProductDto.NumReviews,
                CountInStock = createProductDto.CountInStock,
                Description = createProductDto.Description,
                Reviews = new List<string>(),
                CreateDate = DateTime.UtcNow
            };



            await repository.CreateProduct(newProduct);
            return CreatedAtAction(nameof(CreateProductAsync), new { id = newProduct.Id }, newProduct.AsDto());

        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = (await repository.GetProducts()).Select(item => item.AsDto());
            return products;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(string id)
        {
            try
            {
                var product = await repository.GetProduct(id);
                if (product is null)
                {
                    return BadRequest("Product is not found!");
                }


                if (product.Reviews.Count > 0)
                {
                    var reviewLists = new List<Review>();
                    foreach (var reviewId in product.Reviews)
                    {
                        var review = await reviewRepository.GetReview(reviewId);
                        if (review != null)
                        {
                            reviewLists.Add(review);
                        }
                    }

                    var returnProduct = product with
                    {
                        ReviewsLists = reviewLists
                    };

                    return returnProduct.AsDto();

                }



                return product.AsDto();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        [Authorize]
        [HttpPost("{id}/review")]
        public async Task<ActionResult> CreateProductReivewAsync(string id, [FromBody] CreateReviewDto createReviewDto)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                return Unauthorized("Please login/register to continue");
            }

            var userEmail = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await userRepository.GetUser(userEmail);
            var product = await repository.GetProduct(id);

            if (product is null)
            {
                return BadRequest("Product is not found!");
            }

            if (user is null)
            {
                return Unauthorized();
            }

            Review review = new()
            {
                Comment = createReviewDto.Comment,
                Rating = createReviewDto.Rating,
                User = user.Id,
                Name = user.Name
            };
            await reviewRepository.CreateReview(review);

            IList<string> reviews = new List<string>();
            IList<Review> reviewLists = new List<Review>();
            if (product.Reviews != null)
            {
                reviews = product.Reviews;
            }


            reviews.Add(review.AsDto().Id);

            var productUpdate = product with
            {
                Reviews = reviews
            };

            await repository.UpdateProduct(productUpdate);


            return CreatedAtAction(nameof(CreateProductAsync), new { id = review.Id }, review.AsDto());

        }






    }
}