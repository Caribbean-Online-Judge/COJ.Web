using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Infrestructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllLanguagesQuery : IRequest<Result<IEnumerable<Language>>>
{
}
public sealed class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, Result<IEnumerable<Language>>>
{
    private readonly MainDbContext _db;

    public GetAllLanguagesQueryHandler(MainDbContext db)
    {
        _db = db;
    }

    public async Task<Result<IEnumerable<Language>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var locales = await _db.Languages
                .ToListAsync(cancellationToken: cancellationToken);

            return new Result<IEnumerable<Language>>(locales);
        }
        catch (Exception)
        {
            throw;
        }
    }
}