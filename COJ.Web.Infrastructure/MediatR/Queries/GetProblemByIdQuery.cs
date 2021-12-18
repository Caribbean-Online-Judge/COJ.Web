using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;
public class GetProblemByIdQuery : ILocalizedRequest<Result<ProblemFeatures>>
{
    public int Id { get; set; }
    public string Locale { get; set; }
}



