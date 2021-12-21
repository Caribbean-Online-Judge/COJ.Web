using System.Collections;
using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrastructure.MediatR.Queries;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace COJ.Web.Infrastructure.MediatR.Handlers;

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
        catch (Exception e)
        {
            return new Result<IEnumerable>
            {
                HasError = true,
                Exception = e
            };
        }
    }
}