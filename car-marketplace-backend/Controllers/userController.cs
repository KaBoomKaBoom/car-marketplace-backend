namespace car_marketplace_backend.Controllers
{
    using car_marketplace_backend.Data;
    using car_marketplace_backend.DTOs;
    using car_marketplace_backend.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CarMarketplaceContext _context;

        public UserController(CarMarketplaceContext context)
        {
            _context = context;
        }

        [HttpGet("cars")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var cars = await _context.Cars.ToListAsync();
                if (cars == null || !cars.Any())
                {
                    return NotFound("No cars found.");
                }
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpGet("cars/{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> GetCarById(int id)
        {
            try
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpPost("addCar")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> AddCar([FromBody] AddCarDto addCarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var car = new Car
                {
                    UserId = addCarDto.UserId,
                    Make = addCarDto.Make,
                    Model = addCarDto.Model,
                    Year = addCarDto.Year,
                    Price = addCarDto.Price,
                    Mileage = addCarDto.Mileage,
                    Type = addCarDto.Type,
                    Condition = addCarDto.Condition,
                    Category = addCarDto.Category,
                    Fuel = addCarDto.Fuel,
                    ImageUrl = addCarDto.ImageUrl,
                    HorsePower = addCarDto.HorsePower,
                    Torque = addCarDto.Torque,
                    Transmission = addCarDto.Transmission,
                    Color = addCarDto.Color,
                    Interior = addCarDto.Interior,
                    Drive = addCarDto.Drive,
                    Description = addCarDto.Description,
                    CylinderCapacity = addCarDto.CylinderCapacity,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
    }
}