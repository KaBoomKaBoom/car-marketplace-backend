namespace car_marketplace_backend.Controllers
{
    using car_marketplace_backend.Data;
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
    }
}