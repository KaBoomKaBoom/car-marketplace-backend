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
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }

            return CreatedAtAction(nameof(SignUp), new { id = user.Id }, new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new
            {
                Token = new JwtHelper("your_secret_keygjhzklS:KkcvfhadlKSEWRQ8OiasjvhbjcsmklX;oih", "your_issuer", "your_audience")
                    .GenerateToken(user.Id, user.Username, user.Role)
            });
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody] TokenRequestDto tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = tokenRequest.Role.ToUpper();
            if (role != "ADMIN" && role != "USER")
            {
                return BadRequest("Invalid role. Must be ADMIN or USER");
            }

            // Here you would typically validate the role and generate a token accordingly
            // For simplicity, we are returning a dummy token
            var token = new JwtHelper("your_secret_keygjhzklS:KkcvfhadlKSEWRQ8OiasjvhbjcsmklX;oih", "your_issuer", "your_audience")
                .GenerateToken(0, "dummyUser", role);

            return Ok(new { Token = token });
        }
    }
}
