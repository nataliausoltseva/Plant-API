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

const ImportObject[] API_HEADERS = [
    new ImportObject { key = "id", value = "id" },
    new ImportObject { key = "scientific_name", value = "ScientificName" },
    new ImportObject { key = "rank", value = "Rank" },
    new ImportObject { key = "genus", value = "Genus" },
    new ImportObject { key = "family", value = "Family" },
    new ImportObject { key = "year", value = "Year" },
    new ImportObject { key = "author", value = "Author" },
    new ImportObject { key = "bibliography", value = "Bibliography" },
    new ImportObject { key = "common_name", value = "CommonName" },
    new ImportObject { key = "family_common_name", value = "FamilyCommonName" },
    new ImportObject { key = "image_url", value = "ImageUrl" },
    new ImportObject { key = "flower_color", value = "FlowerColor" },
    new ImportObject { key = "flower_conspicuous", value = "SlowerConsipicuous" },
    new ImportObject { key = "foliage_color", value = "FoliageColor" },
    new ImportObject { key = "foliage_texture", value = "FoliageTexture" },
    new ImportObject { key = "fruit_color", value = "FruitColor" },
    new ImportObject { key = "fruit_conspicuous", value = "FruitConspicuous" },
    new ImportObject { key = "fruit_months", value = "FruitMonths" },
    new ImportObject { key = "bloom_months", value = "BloomMonths" },
    new ImportObject { key = "ground_humidity", value = "GroundHumidity" },
    new ImportObject { key = "growth_form", value = "GrowthForm" },
    new ImportObject { key = "growth_habit", value = "GrowthHabit" },
    new ImportObject { key = "growth_months", value = "GrowthMonths" },
    new ImportObject { key = "growth_rate", value = "GrowthRate" },
    new ImportObject { key = "edible_part", value = "EdiblePart" },
    new ImportObject { key = "vegetable", value = "Vegetable" },
    new ImportObject { key = "edible", value = "Edible" },
    new ImportObject { key = "light", value = "Light" },
    new ImportObject { key = "soil_nutriments", value = "SoilNutriments" },
    new ImportObject { key = "soil_salinity", value = "SoilSalinity" },
    new ImportObject { key = "anaerobic_tolerance", value = "AnaerobicTolerance" },
    new ImportObject { key = "atmospheric_humidity", value = "AnaerobicHumidity" },
    new ImportObject { key = "average_height_cm", value = "AverageHeightCm" },
    new ImportObject { key = "maximum_height_cm", value = "MaximumHeightCm" },
    new ImportObject { key = "minimum_root_depth_cm", value = "MinimumRootDepthCm" },
    new ImportObject { key = "ph_maximum", value = "PhMaximum" },
    new ImportObject { key = "ph_minimum", value = "PhMinimum" },
    new ImportObject { key = "planting_days_to_harvest", value = "PlantingDaysToHarvest" },
    new ImportObject { key = "planting_description", value = "PlantingDescription" },
    new ImportObject { key = "planting_sowing_description", value = "PlantingSowingDescription" },
    new ImportObject { key = "planting_row_spacing_cm", value = "PlantingRowSpacingCm" },
    new ImportObject { key = "planting_spread_cm", value = "PlantingSpreadCm" },
    new ImportObject { key = "synonyms", value = "Synonyms" },
    new ImportObject { key = "distributions", value = "Distributions" },
    new ImportObject { key = "common_names", value = "CommonNames" },
    new ImportObject { key = "url_usda", value = "UrlUsda" },
    new ImportObject { key = "url_tropicos", value = "UrlTropicos" },
    new ImportObject { key = "url_tela_botanica", value = "UrlTelaBotanica" },
    new ImportObject { key = "url_powo", value = "UrlPowo" },
    new ImportObject { key = "url_plantnet", value = "UrlPlantnet" },
    new ImportObject { key = "url_gbif", value = "UrlGbif" },
    new ImportObject { key = "url_openfarm", value = "UrlOpenfarm" },
    new ImportObject { key = "url_catminat", value = "UrlCatminat" },
    new ImportObject { key = "url_wikipedia_en", value = "UrlWikipediaEn" },
];

using (var reader = new StreamReader(@"C:\Development\species.csv"))
{
    Int32 counter = 0;
    string[] headers = [];
    List<Species> rowsLines = new List<Species>();

    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();

        if (counter == 0)
        {
            headers = line.Split('\t');
        } else
        {
            Int16 headerCounter = 0;
            Species species = new Species();
            var values = line.Split('\t');
            foreach (var item in headers)
            {
                string value = API_HEADERS.SingleOrDefault(item => item.key == item).value;
                species[value] = values[headerCounter];
                headerCounter++;
            }
            rowsLines.Add(species);
            if (counter == 1)
            {
                foreach (var item in rowsLines)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
        counter++;
    }
}

app.Run();

class ImportObject
{
    public string key { get; set; }
    public string value { get; set; }
}