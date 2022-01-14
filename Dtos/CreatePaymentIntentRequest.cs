using Newtonsoft.Json;

namespace backend.Dtos
{
    public record CreatePaymentIntenetRequest
    {
        [JsonProperty("amount")]
        public long Amount { get; init; }
    }
}