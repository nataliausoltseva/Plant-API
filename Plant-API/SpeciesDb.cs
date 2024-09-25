using Microsoft.EntityFrameworkCore;

namespace Plant_API
{
    public class SpeciesDb : DbContext
    {
        public SpeciesDb(DbContextOptions<SpeciesDb> options) : base(options) { }
        public DbSet<Species> Species => Set<Species>();
    }
}