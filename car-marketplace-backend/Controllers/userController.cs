namespace car_marketplace_backend.Controllers
{
    using System.Security.Claims;
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

        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] AddCarDto updateCarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }
                if (car.UserId != updateCarDto.UserId)
                {
                    return Forbid("You can only update your own cars.");
                }

                car.Make = updateCarDto.Make;
                car.Model = updateCarDto.Model;
                car.Year = updateCarDto.Year;
                car.Price = updateCarDto.Price;
                car.Mileage = updateCarDto.Mileage;
                car.Type = updateCarDto.Type;
                car.Condition = updateCarDto.Condition;
                car.Category = updateCarDto.Category;
                car.Fuel = updateCarDto.Fuel;
                car.ImageUrl = updateCarDto.ImageUrl;
                car.HorsePower = updateCarDto.HorsePower;
                car.Torque = updateCarDto.Torque;
                car.Transmission = updateCarDto.Transmission;
                car.Color = updateCarDto.Color;
                car.Interior = updateCarDto.Interior;
                car.Drive = updateCarDto.Drive;
                car.Description = updateCarDto.Description;
                car.CylinderCapacity = updateCarDto.CylinderCapacity;

                _context.Cars.Update(car);
                await _context.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
        [HttpDelete("deleteCar/{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpGet("profile")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }

            try
            {
                var user = await _context.Users.FindAsync(int.Parse(userId));
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
    }
}