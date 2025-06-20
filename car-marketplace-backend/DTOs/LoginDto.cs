﻿using System.ComponentModel.DataAnnotations;

namespace car_marketplace_backend.DTOs
{
    public class LoginDto
    {
        [Required][EmailAddress] 
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
