using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Dtos
{
    public record ReviewDto
    {

        public string? Id { get; init; }

        [Required]
        public string User { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, 5)]
        public int Rating { get; init; }
        [Required]
        public string Comment { get; init; }
    }
}