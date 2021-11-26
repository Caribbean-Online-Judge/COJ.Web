using COJ.Web.Infrestructure.Data;
using COJ.Web.Seeder.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Caribbean Online Judge Seeder");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
       {
           services.AddTransient<CountriesSeeder>();
           services.AddTransient<InstitutionsSeeder>();
           services.AddTransient<LocaleSeeder>();
           services.AddTransient<LanguagesSeeder>();

           services.AddDbContext<MainDbContext>(options =>
               options.UseNpgsql(args[0], b => b.MigrationsAssembly("COJ.Web.API")));
       })
    .ConfigureLogging(options =>
    {

    })
    .Build();

Seed<CountriesSeeder>(host.Services);
Seed<InstitutionsSeeder>(host.Services);
Seed<LocaleSeeder>(host.Services);
Seed<LanguagesSeeder>(host.Services);

host.Run();

static void Seed<T>(IServiceProvider services) where T : ISeeder
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    var seeder = provider.GetRequiredService<T>();
    seeder.Seed();
}