public class Car
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Fuel { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public decimal HorsePower { get; set; }
    public decimal Torque { get; set; }
    public string Transmission { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Interior  { get; set; } = string.Empty;
    public string Drive { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Mileage { get; set; }
    public decimal CylinderCapacity { get; set; }
}