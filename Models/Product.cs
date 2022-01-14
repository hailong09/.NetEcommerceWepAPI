using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public record Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }
        public string Name { get; init; }
        public string Brand { get; init; }
        public string Image { get; init; }
        public string Category { get; init; }
        public Decimal Price { get; init; }
        public Decimal Rating { get; init; }
        public int NumReviews { get; init; }
        public int CountInStock { get; init; }
        public string Description { get; init; }
        public DateTime CreateDate { get; init; }

        [BsonRepresentation(BsonType.ObjectId)]
        public IList<string>? Reviews { get; init; }

        [BsonIgnore]
        public IList<Review>? ReviewsLists { get; init; }

    }
}