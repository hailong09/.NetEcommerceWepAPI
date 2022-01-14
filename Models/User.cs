using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public record User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }
        public string Name { get; init; }
        [EmailAddress]
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTime CreateDate { get; init; }
    }
}