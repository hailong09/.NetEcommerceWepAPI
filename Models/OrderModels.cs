namespace backend.Models
{
    public record OrderModels
    {
        public string Id { get; init; }
        public string UserId { get; init; }
        public List<CartItem> CartItems { get; init; }

        public CustomerInfo CustomerInfo { get; init; }
        public Decimal Amount { get; init; }

        public DateTime? Created { get; init; }
    }
}