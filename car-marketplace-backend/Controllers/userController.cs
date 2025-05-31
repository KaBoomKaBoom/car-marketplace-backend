namespace car_marketplace_backend.Controllers
{
    using car_marketplace_backend.Data;
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
    }
}