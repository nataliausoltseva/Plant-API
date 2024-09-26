using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Plant_API.Controllers
{
    [ApiController]
    [Route("/species/search")]
    public class SpeciesSearchController(SpeciesDb context) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> SearchSpecies([FromQuery] string? q, [FromQuery] int? page)
        {
            if (!String.IsNullOrEmpty(q))
            {

                int startIndex = (int)(page != null ? (page - 1) * 10 : 0);
                var species = await context
                    .Species
                    .Where(s => s.CommonName == null ? false : s.CommonName.ToUpper().Contains(q.ToUpper()))
                    .Skip(startIndex)
                    .Take(10)
                    .ToListAsync();
                return Ok(species);
            }

            return NotFound();
        }
    }
}
