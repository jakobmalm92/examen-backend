using examensarbeteBackend.Data;
using examensarbeteBackend.Models;
using examensarbeteBackend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(AppDbContext context, JwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest(new { error = "E-postadressen är redan registrerad." });
            }

            // Hasha lösenordet och spara användaren
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Användare registrerad!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ett oväntat fel inträffade.", details = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
            {
                return Unauthorized(new { error = "Felaktiga inloggningsuppgifter." });
            }

            // Generera JWT-token
            var token = _jwtTokenService.GenerateToken(existingUser.Email, existingUser.Id);

            // Lägg till token i headers
            Response.Headers.Append("Authorization", $"Bearer {token}");

            // Returnera token i body
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ett oväntat fel inträffade.", details = ex.Message });
        }
    }



}
