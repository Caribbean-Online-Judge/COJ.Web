using System.Collections;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllInstitutionsQuery : IRequest<Result<IEnumerable>>
{
    public bool IsForPublic { get; set; }
    public int? CountryId { get; set; }
}

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
        catch (Exception)
        {
            throw;
        }
    }
}