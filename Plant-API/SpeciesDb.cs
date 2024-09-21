using Microsoft.EntityFrameworkCore;

class SpeciesDb : DbContext
{
    public SpeciesDb(DbContextOptions<SpeciesDb> options) : base(options) { }
    public DbSet<Species> Species => Set<Species>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new DbInitializer(modelBuilder).Seed();
    }
}