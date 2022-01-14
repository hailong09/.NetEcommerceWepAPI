using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record LoginDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
    }
}