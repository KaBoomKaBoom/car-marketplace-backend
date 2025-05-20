using System.ComponentModel.DataAnnotations;

namespace car_marketplace_backend.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
