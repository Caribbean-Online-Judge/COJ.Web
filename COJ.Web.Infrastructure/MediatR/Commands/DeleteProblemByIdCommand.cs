using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class DeleteProblemByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
    public bool DeleteRelatedData { get; set; }
}