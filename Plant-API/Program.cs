namespace Plant_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SpeciesDb>();
                DataImporter dataimporter = new DataImporter();
                dbContext.Species.AddRange(dataimporter.ImportCsv().ToArray());
                dbContext.SaveChanges();
                dbContext.Database.EnsureCreated();
            }

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}