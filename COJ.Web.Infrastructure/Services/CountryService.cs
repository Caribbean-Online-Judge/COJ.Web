using System.Collections;
using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.MediatR;
using COJ.Web.Infrastructure.MediatR.Queries;
using MediatR;

namespace COJ.Web.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly IMediator _mediator;

    public CountryService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<Result<IEnumerable>> GetAll(bool isForPublic = true)
    {
        var result = await _mediator.Send(new GetAllCountriesQuery()
        {
            IsForPublic = isForPublic
        });
        return result;
    }
}