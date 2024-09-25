using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Plant_API.Controllers
{
    [ApiController]
    [Route("/species/search")]
    public class SpeciesSearchController(SpeciesDb context) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> SearchSpecies([FromQuery] string? q)
        {
            if (!String.IsNullOrEmpty(q))
            {
                var species = await context
                    .Species
                    .Where(s => s.CommonName == null ? false : s.CommonName.ToUpper().Contains(q.ToUpper())).ToListAsync();
                return Ok(species);
            }

            return NotFound();
        }
    }
}
