using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public record Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string User { get; init; }
        public string Name { get; init; }
        public int Rating { get; init; }
        public string Comment { get; init; }
    }
}