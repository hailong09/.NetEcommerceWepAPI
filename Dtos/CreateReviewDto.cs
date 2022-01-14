using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record CreateReviewDto
    {
        [Required]
        public string Comment { get; init; }
        [Required]
        public int Rating { get; init; }
    }
}