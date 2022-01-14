using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.Dtos
{
    public record OrderRequestDto
    {
        [Required]
        public string Id { get; init; }

        [Required]
        public string UserId { get; init; }

        [Required]
        public List<CartItem> CartItems { get; init; }
        
        [Required]
        public CustomerInfo CustomerInfo { get; init; }
        [Required]
        public Decimal Amount { get; init; }


    }
}