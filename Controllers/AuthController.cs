using Microsoft.AspNetCore.Mvc;
using examensarbeteBackend.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Kontrollera om e-post och lösenord är korrekta
        if (request.Email == "test@example.com" && request.Password == "password123")
        {
            return Ok(new { message = "Inloggning lyckades!" });
        }

        // Om inloggningsuppgifterna är felaktiga
        return Unauthorized(new { error = "Felaktiga inloggningsuppgifter." });
    }
}

