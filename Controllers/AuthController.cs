using examensarbeteBackend.Data;
using examensarbeteBackend.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            // Kontrollera om e-postadressen redan finns
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
            // Returnera JSON-svar vid fel
            return StatusCode(500, new { error = "Ett oväntat fel inträffade.", details = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
        {
            return Unauthorized(new { error = "Felaktiga inloggningsuppgifter." });
        }

        return Ok(new { message = "Inloggning lyckades!" });
    }
}