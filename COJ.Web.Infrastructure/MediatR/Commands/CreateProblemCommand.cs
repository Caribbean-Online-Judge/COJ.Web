using COJ.Web.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace COJ.Web.Infrastructure.MediatR.Commands;

public class CreateProblemCommand : IRequest<Problem>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }

    public double Points { get; set; }
    public int TimeLimit { get; set; }
    public int MemoryLimit { get; set; }
    public int CaseTimeLimit { get; set; }
    public int OutputLimit { get; set; }
    public int SourceCodeLengthLimit { get; set; }
    public bool Multidata { get; set; }
    public bool SpecialJudge { get; set; }

    public int ClassificationId { get; set; }
    public IList<IFormFile> InputFiles { get; set; }
    public IList<IFormFile> OutputFiles { get; set; }
}