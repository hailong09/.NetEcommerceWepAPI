using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Dtos
{
    public record CreatePaymentIntentResponseDto
    {

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; init; }
    }
}