using Microsoft.AspNetCore.Mvc;
using OfferApi.Models;

namespace Test1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
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

            Console.WriteLine($"Offertförfrågan: {request.Name} - {request.Service}");

            return Ok(new { message = "Din offertförfrågan har skickats!" });
        }
    }
}
