using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record UserDto
    {
        public string? Id { get; init; }
        public string Name { get; init; }
        [EmailAddress]
        public string Email { get; init; }
        public DateTime CreateDate { get; init; }


    }
}