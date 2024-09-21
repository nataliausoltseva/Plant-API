using Microsoft.EntityFrameworkCore;

public class DbInitializer
{
	private readonly ModelBuilder modelBuilder;

	public DbInitializer(ModelBuilder modelBuilder)
	{
		this.modelBuilder = modelBuilder;
	}

	public void Seed()
	{
		using (var reader = new StreamReader(@"C:\Development\species.csv"))
		{
			List<string> listA = new List<string>();
			List<string> listB = new List<string>();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split('\t');

				listA.Add(values[0]);
				listB.Add(values[1]);
			}

			foreach (var item in listA)
			{
				Console.WriteLine(item.ToString());
			}

			foreach (var item in listB)
			{
				Console.WriteLine(item.ToString());
			}

		}


		//modelBuilder.Entity<User>().HasData(
		//	   new User() { Id = 1.... },
		//	   new User() { Id = 2.... },
		//);
	}
}
