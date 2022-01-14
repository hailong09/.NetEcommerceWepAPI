namespace backend.Models
{
    public record CartItem
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public long Quantity { get; set; }

        public string Image { get; init; }
        public Decimal Price { get; set; }


    }
}