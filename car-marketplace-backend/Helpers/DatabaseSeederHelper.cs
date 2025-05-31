using System.Text.Json;
using car_marketplace_backend.Data;
using car_marketplace_backend.DTOs;
using car_marketplace_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace car_marketplace_backend.Helpers
{
    public class DatabaseSeederHelper
    {
        private readonly CarMarketplaceContext _context;

        public DatabaseSeederHelper(CarMarketplaceContext context)
        {
            _context = context;
        }

        public async Task SeedAsync(string jsonFilePath)
        {
            try
            {
                // Read JSON file
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"JSON file not found at: {jsonFilePath}");
                }

                var jsonData = await File.ReadAllTextAsync(jsonFilePath);
                var carsToSeed = JsonSerializer.Deserialize<List<AddCarDto>>(jsonData);

                if (carsToSeed == null || !carsToSeed.Any())
                {
                    throw new InvalidOperationException("No cars found in JSON file or JSON is invalid");
                }

                // Check if database already has cars
                if (await _context.Cars.AnyAsync())
                {
                    Console.WriteLine("Database already contains cars, skipping seeding");
                    return;
                }

                // Get a default user ID (assuming at least one user exists)
                var defaultUser = await _context.Users.FirstOrDefaultAsync();
                if (defaultUser == null)
                {
                    throw new InvalidOperationException("No users found in database. Please seed users first.");
                }

                foreach (var carDto in carsToSeed)
                {
                    var car = new Car
                    {
                        UserId = defaultUser.Id,
                        Make = carDto.Make,
                        Model = carDto.Model,
                        Year = carDto.Year,
                        Price = carDto.Price,
                        Mileage = carDto.Mileage,
                        Type = carDto.Type,
                        Condition = carDto.Condition,
                        Category = carDto.Category,
                        Fuel = carDto.Fuel,
                        ImageUrl = carDto.ImageURL,
                        HorsePower = carDto.HorsePower,
                        Torque = carDto.Torque,
                        Transmission = carDto.Transmission,
                        Color = carDto.Color,
                        Interior = carDto.Interior,
                        Drive = carDto.Drive,
                        CylinderCapacity = carDto.CylinderCapacity,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Cars.Add(car);
                }

                await _context.SaveChangesAsync();
                Console.WriteLine($"Successfully seeded {carsToSeed.Count} cars into the database");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding database: {ex.Message}");
                throw;
            }
        }
    }
}