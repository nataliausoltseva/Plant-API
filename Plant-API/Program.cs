using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SpeciesDb>(opt => opt.UseInMemoryDatabase("SpeciesList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SpeciesDb>();
    DataImporter dataimporter = new DataImporter();
    dbContext.Species.AddRange(dataimporter.ImportCsv().ToArray());
    dbContext.SaveChanges();
    dbContext.Database.EnsureCreated();
}

app.MapGet("/species/{id}", async (int id, SpeciesDb db) =>
    await db.Species.FindAsync(id)
        is Species species
            ? Results.Ok(species)
            : Results.NotFound()
);

app.Run();