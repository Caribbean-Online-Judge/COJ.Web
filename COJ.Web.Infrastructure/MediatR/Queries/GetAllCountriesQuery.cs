using System.Collections;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllCountriesQuery : IRequest<Result<IEnumerable>>
{
    public bool IsForPublic { get; set; }
}

public sealed class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, Result<IEnumerable>>
{
    private readonly MainDbContext _db;

    public GetAllCountriesQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Result<IEnumerable>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable countries;
            if (request.IsForPublic)
                countries = await _db.Countries
                    .Select(x => new PublicCountryDto(x.Id, x.Name, x.ISOCode))
                    .ToListAsync(cancellationToken: cancellationToken);
            else
                countries = await _db.Countries
                    .ToListAsync(cancellationToken: cancellationToken);
            return new Result<IEnumerable>(countries);
        }
        catch (Exception)
        {
            throw;
        }
    }
}