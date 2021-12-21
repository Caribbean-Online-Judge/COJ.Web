using COJ.Web.Domain.Entities;
using COJ.Web.Infrestructure.Data;
using COJ.Web.Seeder.Utils;

namespace COJ.Web.Seeder.Seeders;
internal class LocaleSeeder : ISeeder
{
    private readonly string FILE_PATH = $@"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Locales.csv";

    public LocaleSeeder(MainDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public MainDbContext DbContext { get; }

    public void Seed()
    {
        if (DbContext.Locales.Count() != 0)
        {
            ConsoleUtils.WriteLineWarning("The table is not empty, so seed operations can insert duplicate data");
        }

        if (!File.Exists(FILE_PATH))
        {
            ConsoleUtils.WriteLineFatalError("FATAL ERROR: The Locales.csv file is missing in Data directory.");
            return;
        }

        var entries = CsvLoader.Load(FILE_PATH);

        foreach (var item in entries)
        {
            var id = int.Parse(item[0]);
            if (DbContext.Locales.SingleOrDefault(x => x.Id == id) != null)
            {
                Console.WriteLine("Skipping...");
                continue;
            }

            var entity = new Locale()
            {
                Id = id,
                Code = item[1],
                Description = item[2],
            };
            DbContext.Locales.Add(entity);
        }

        DbContext.SaveChanges();

        Console.WriteLine("Locales seed complete!");
    }
}

