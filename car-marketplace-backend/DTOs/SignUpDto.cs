using System.ComponentModel.DataAnnotations;

namespace car_marketplace_backend.DTOs
{
    public class SignUpDto
    {
        [Required][StringLength(50)] 
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public string? Role { get; set; }
    }
}
