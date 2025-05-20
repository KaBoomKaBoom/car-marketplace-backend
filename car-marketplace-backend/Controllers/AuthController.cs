using car_marketplace_backend.Data;
using car_marketplace_backend.DTOs;
using car_marketplace_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace car_marketplace_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly CarMarketplaceContext _context;

        public AuthController(CarMarketplaceContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public IActionResult SignUp([FromBody] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == signUpDto.Email))
            {
                return BadRequest("Email already exists");
            }

            var role = signUpDto.Role?.ToUpper() ?? "USER";
            if (role != "ADMIN" && role != "USER")
            {
                return BadRequest("Invalid role. Must be ADMIN or USER");
            }

            var user = new User
            {
                Id = _context.Users.Any() ? _context.Users.Max(u => u.Id) + 1 : 1,
                Username = signUpDto.Username,
                Email = signUpDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password),
                Role = role
            };

            _context.Users.Add(user);

            return CreatedAtAction(nameof(SignUp), new { id = user.Id }, new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            });
        }
    }
}
