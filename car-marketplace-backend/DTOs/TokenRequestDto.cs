using System.ComponentModel.DataAnnotations;

namespace car_marketplace_backend.DTOs
{
    public class TokenRequestDto
    {
        [Required] 
        public string Role { get; set; } = string.Empty;
    }
}
