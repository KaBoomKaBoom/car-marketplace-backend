using car_marketplace_backend.Data;
using car_marketplace_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace car_marketplace_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly CarMarketplaceContext _context;

        public AdminController(CarMarketplaceContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _context.Users.ToList();
                if (users == null || !users.Any())
                {
                    return NotFound("No users found.");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpGet("users/{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpPost("addUser")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                user.Role = "USER"; // Default role for new users
                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
        [HttpPut("updateUser/{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var existingUser = _context.Users.Find(id);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Role = user.Role; // Allow role update

                _context.Users.Update(existingUser);
                _context.SaveChanges();
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpDelete("deleteUser/{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpGet("cars")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetCars()
        {
            try
            {
                var cars = _context.Cars.ToList();
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
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetCarById(int id)
        {
            try
            {
                var car = _context.Cars.Find(id);
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
        [Authorize(Roles = "ADMIN")]
        public IActionResult AddCar([FromBody] Car car)
        {
            if (car == null || string.IsNullOrEmpty(car.Make) || string.IsNullOrEmpty(car.Model) || car.Price <= 0)
            {
                return BadRequest("Invalid car data.");
            }

            try
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult UpdateCar(int id, [FromBody] Car car)
        {
            if (car == null || string.IsNullOrEmpty(car.Make) || string.IsNullOrEmpty(car.Model) || car.Price <= 0)
            {
                return BadRequest("Invalid car data.");
            }

            try
            {
                var existingCar = _context.Cars.Find(id);
                if (existingCar == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }

                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.Price = car.Price;
                existingCar.Mileage = car.Mileage;
                existingCar.Color = car.Color;
                existingCar.Transmission = car.Transmission;
                existingCar.Fuel = car.Fuel;
                existingCar.HorsePower = car.HorsePower;
                existingCar.Torque = car.Torque;
                existingCar.Interior = car.Interior;
                existingCar.Drive = car.Drive;
                existingCar.Category = car.Category;
                existingCar.Description = car.Description;
                existingCar.ImageUrl = car.ImageUrl;

                _context.Cars.Update(existingCar);
                _context.SaveChanges();
                return Ok(existingCar);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
        [HttpDelete("deleteCar/{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                var car = _context.Cars.Find(id);
                if (car == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }

                _context.Cars.Remove(car);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }
    }
}