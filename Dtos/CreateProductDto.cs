using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record CreateProductDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Image { get; init; }
        [Required]
        public string Category { get; init; }
        [Required]
        public Decimal Price { get; init; }
       
        public string Brand { get; init; }
        [Required]
        public int Rating { get; init; }
        [Required]
        public int NumReviews { get; init; }
        [Required]
        public int CountInStock { get; init; }
        [Required]
        public string Description { get; init; }

    }
}