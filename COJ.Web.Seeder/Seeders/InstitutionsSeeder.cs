using COJ.Web.Domain.Entities;
using COJ.Web.Infrestructure.Data;
using COJ.Web.Seeder.Utils;

namespace COJ.Web.Seeder.Seeders;
internal class InstitutionsSeeder : ISeeder
{
    private readonly string FILE_PATH = $@"{Environment.CurrentDirectory}\Data\Institutions.csv";

    public InstitutionsSeeder(MainDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public MainDbContext DbContext { get; }

    public void Seed()
    {
        if (DbContext.Countries.Count() != 0)
        {
            ConsoleUtils.WriteLineWarning("The table is not empty, so seed operations can insert duplicate data");
        }

        if (!File.Exists(FILE_PATH))
        {
            ConsoleUtils.WriteLineFatalError("FATAL ERROR: The Institutions.csv file is missing in Data directory.");
            return;
        }

        var entries = CsvLoader.Load(FILE_PATH);

        foreach (var item in entries)
        {
            var id = int.Parse(item[0]);
            if (DbContext.Institutions.SingleOrDefault(x => x.Id == id) != null)
            {
                Console.WriteLine("Skipping...");
                continue;
            }

            var country = DbContext.Countries.Single(x => x.Id == int.Parse(item[2]));
            var entity = new Institution()
            {
                Id = id,
                Name = item[1],
                Country = country,
                Enabled = bool.Parse(item[3]),
            };
            DbContext.Institutions.Add(entity);
        }

        DbContext.SaveChanges();

        Console.WriteLine("Institutions seed complete!");
    }
}

