using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record RegisterDto
    {
        [Required]
        public string Name { get; init; }
        [EmailAddress]
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }

    }
}