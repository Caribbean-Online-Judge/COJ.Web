using COJ.Web.Domain.Entities;
using COJ.Web.Infrestructure.Data;
using COJ.Web.Seeder.Utils;

namespace COJ.Web.Seeder.Seeders;
internal class LanguagesSeeder : ISeeder
{
    private readonly string FILE_PATH = $@"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Languages.csv";

    public LanguagesSeeder(MainDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public MainDbContext DbContext { get; }

    public void Seed()
    {
        if (DbContext.Languages.Count() != 0)
        {
            ConsoleUtils.WriteLineWarning("The table is not empty, so seed operations can insert duplicate data");
        }

        if (!File.Exists(FILE_PATH))
        {
            ConsoleUtils.WriteLineFatalError("FATAL ERROR: The Languages.csv file is missing in Data directory.");
            return;
        }

        var entries = CsvLoader.Load(FILE_PATH);

        foreach (var item in entries)
        {
            var id = int.Parse(item[0]);
            if (DbContext.Languages.SingleOrDefault(x => x.Id == id) != null)
            {
                Console.WriteLine("Skipping...");
                continue;
            }

            var entity = new Language()
            {
                Id = id,
                Name = item[1],
                Enabled = bool.Parse(item[2]),
                Extension = item[3],
                Key = item[4],
                Prority = int.Parse(item[5]),
                Description = item.Length == 7 ? item[6] : string.Empty,
                
            };
            DbContext.Languages.Add(entity);
        }

        DbContext.SaveChanges();

        Console.WriteLine("Languages seed complete!");
    }
}

