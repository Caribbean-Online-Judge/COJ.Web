using COJ.Web.Domain.Abstract;
using COJ.Web.Infrestructure.Data;

namespace COJ.Web.Infrastructure.Services;

public class CountryService : ICountryService
{

    public CountryService(MainDbContext db)
    {
        Db = db;
    }

    public MainDbContext Db { get; }

    public IQueryable GetAll(bool isForPublic = true)
    {
        return isForPublic ? Db.Countries.Select(x => new
        {
            x.Id,
            x.Name,
            x.ISOCode
        }) : Db.Countries;
    }
}