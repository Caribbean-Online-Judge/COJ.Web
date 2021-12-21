using COJ.Web.Domain.Entities;
using COJ.Web.Infrestructure.Data;
using COJ.Web.Seeder.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Seeder.Seeders
{
    internal class CountriesSeeder : ISeeder
    {
        private readonly string FILE_PATH = $@"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Countries.csv";

        public CountriesSeeder(MainDbContext dbContext)
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
                ConsoleUtils.WriteLineFatalError("FATAL ERROR: The Countries.csv file is missing in Data directory.");
                return;
            }

            var entries = CsvLoader.Load(FILE_PATH);

            foreach (var item in entries)
            {
                var id = int.Parse(item[0]);
                if (DbContext.Countries.SingleOrDefault(x => x.Id == id) != null)
                {
                    Console.WriteLine("Skipping...");
                    continue;
                }

                var entity = new Country()
                {
                    Id = id,
                    Name = item[1],
                    ISOCode = item[2],
                    Enabled = bool.Parse(item[3]),
                };
                DbContext.Countries.Add(entity);
            }

            DbContext.SaveChanges();

            Console.WriteLine("Countries seed complete!");
        }
    }
}
