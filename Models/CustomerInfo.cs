namespace backend.Models
{
    public record CustomerInfo
    {
        public string EmailAddress { get; init; }
        public string FullName { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string StateOrProvince { get; init; }
        public string PostalCode { get; init; }

    }
}