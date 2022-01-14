using System;
using System.ComponentModel.DataAnnotations;
using backend.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Dtos
{
    public record ProductDto
    {
        public string? Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Image { get; init; }
        [Required]
        public string Category { get; init; }
        [Required]
        public Decimal Price { get; init; }

        public string Brand { get; init; }
        [Required]
        public Decimal Rating { get; init; }
        [Required]
        public int NumReviews { get; init; }
        [Required]
        public int CountInStock { get; init; }
        [Required]
        public string Description { get; init; }

        public DateTime CreateDate { get; init; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IList<string>? Reviews { get; init; }

        public IList<Review> ReviewsLists { get; init; }
    }
}