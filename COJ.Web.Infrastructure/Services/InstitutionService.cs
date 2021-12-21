using System.Collections;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.MediatR;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;

namespace COJ.Web.Infrastructure.Services;

public class InstitutionService : IInstitutionService
{
    private readonly IMediator _mediator;

    public InstitutionService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<Result<IEnumerable>> GetAll(int? countryId, bool isForPublic = true)
    {
        var result = await _mediator.Send(new GetAllInstitutionsQuery
        {
            IsForPublic = isForPublic,
            CountryId = countryId,
        });
        return result;
    }
}