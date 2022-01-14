using backend.Models;

namespace backend.Dtos
{
    public record OrderDto
    {
        public string Id { get; init; }
        public string UserId { get; init; }
        public List<CartItem> CartItems { get; init; }

        public CustomerInfo CustomerInfo { get; init; }

        public Decimal Amount { get; init; }

        public DateTime? Created { get; init; }
    }
}