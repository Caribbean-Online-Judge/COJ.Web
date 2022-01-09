using System.Collections;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models.Dtos;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllLocalesQuery : IRequest<Result<IEnumerable>>
{
    public bool IsForPublic { get; set; }
}

public sealed class GetAllLocalesQueryHandler : IRequestHandler<GetAllLocalesQuery, Result<IEnumerable>>
{
    private readonly MainDbContext _db;

    public GetAllLocalesQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Result<IEnumerable>> Handle(GetAllLocalesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable locales;
            if (request.IsForPublic)
            {
                locales = await _db.Locales
                    .Select(x => new PublicLocaleDto(x.Id, x.Code, x.Description))
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else
            {
                locales = await _db.Locales
                    .ToListAsync(cancellationToken: cancellationToken);
            }

            return new Result<IEnumerable>(locales);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}