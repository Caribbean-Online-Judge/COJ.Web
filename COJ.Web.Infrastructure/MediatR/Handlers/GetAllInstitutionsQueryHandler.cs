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

public sealed class GetAllInstitutionsQueryHandler : IRequestHandler<GetAllInstitutionsQuery, Result<IEnumerable>>
{
    private readonly MainDbContext _db;

    public GetAllInstitutionsQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Result<IEnumerable>> Handle(GetAllInstitutionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable institutions;
            if (request.IsForPublic)
                institutions = await _db.Institutions
                    .Where(x => x.Enabled && (!request.CountryId.HasValue || x.CountryId == request.CountryId))
                    .Select(x => new PublicInstitutionDto(x.Id, x.CountryId, x.Name))
                    .ToListAsync(cancellationToken: cancellationToken);
            else
                institutions = await _db.Institutions
                    .ToListAsync(cancellationToken: cancellationToken);
            return new Result<IEnumerable>(institutions);
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