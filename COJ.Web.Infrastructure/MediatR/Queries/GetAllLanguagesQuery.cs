using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;

public class GetAllLanguagesQuery : IRequest<Result<IEnumerable<Language>>>
{
}