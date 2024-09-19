using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SpeciesDb>(opt => opt.UseInMemoryDatabase("SpeciesList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/species", async(SpeciesDb db) => 
    await db.Species.ToListAsync()
);

app.Run();
