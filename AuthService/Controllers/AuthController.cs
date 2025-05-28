using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtTokenService _jwtService;

        public AuthController(ApplicationDbContext context, IJwtTokenService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // POST: /api/auth/register
        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Bu email allaqachon ro'yxatdan o'tgan.");

            var passwordHasher = new PasswordHasher();
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHasher.Hash(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Ro'yxatdan o'tish muvaffaqiyatli.");
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return Unauthorized("Email noto‘g‘ri.");

            var passwordHasher = new PasswordHasher();
            if (!passwordHasher.Verify(user.PasswordHash, dto.Password))
                return Unauthorized("Parol noto‘g‘ri.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        // GET: /api/auth/profile
        [HttpGet("profile")]
        [Authorize(Roles = "Physician,Nurse,Patient,Admin")]
        public async Task<IActionResult> Profile()
        {
            var email = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role
            });
        }
    }
}
