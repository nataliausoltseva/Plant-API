using Microsoft.EntityFrameworkCore;

class SpeciesDb : DbContext
{
    public SpeciesDb(DbContextOptions<SpeciesDb> options) : base(options) { }
    public DbSet<Species> Species => Set<Species>();
}