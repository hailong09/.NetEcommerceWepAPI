using Newtonsoft.Json;

namespace backend.Dtos
{
    public record ConfigResponseDto
    {
        [JsonProperty("publishableKey")]
        public string PublishableKey { get; init; }
    }
}