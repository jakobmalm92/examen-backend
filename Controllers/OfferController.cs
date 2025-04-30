using examensarbeteBackend.Data;
using examensarbeteBackend.Models;
using Microsoft.AspNetCore.Mvc;
using OfferApi.Models;

namespace examensarbeteBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OfferController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] OfferRequest request)
        {
            if (string.IsNullOrEmpty(request.Name) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Phone) ||
                string.IsNullOrEmpty(request.Description))
            {
                return BadRequest(new { error = "Alla fält måste fyllas i." });
            }

            // Spara offertförfrågan i databasen
            _context.OfferRequests.Add(request);
            _context.SaveChanges();

            return Ok(new { message = "Din offertförfrågan har sparats!" });
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Hämta alla offertförfrågningar från databasen
            var requests = _context.OfferRequests.ToList();
            return Ok(requests);
        }
    }
}

