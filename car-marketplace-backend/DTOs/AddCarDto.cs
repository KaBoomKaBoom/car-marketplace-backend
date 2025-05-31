using System.ComponentModel.DataAnnotations;

namespace car_marketplace_backend.DTOs
{
    public class AddCarDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Make is required.")]
        public string Make { get; set; } = string.Empty;
        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;
        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Mileage is required.")]
        public decimal Mileage { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Fuel { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal HorsePower { get; set; }
        public decimal Torque { get; set; }
        public string Transmission { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Interior { get; set; } = string.Empty;
        public string Drive { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal CylinderCapacity { get; set; }
    }
}