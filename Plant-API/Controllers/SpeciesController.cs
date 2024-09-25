using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Plant_API.Controllers
{
    [ApiController]
    [Route("/species")]
    public class SpeciesController(SpeciesDb context) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetSpecies([FromQuery] string? page)
        {
            var species = await context.Species.Take(10).ToListAsync();
            return Ok(species);
        }
    }
}
