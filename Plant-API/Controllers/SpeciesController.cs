using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Plant_API.Controllers
{
    [ApiController]
    [Route("/species")]
    public class SpeciesController(SpeciesDb context) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetSpeciesByPage([FromQuery] string? page)
        {
            var species = await context.Species.Take(10).ToListAsync();
            return Ok(species);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSpeciesById([FromRoute] int id)
        {
            var species = await context.Species.FindAsync(id);

            if (species == null)
            {
                return NotFound();
            }
            return Ok(species);
        }
    }
}
