using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Values;
using COJ.Web.Infrastructure.Extensions;
using COJ.Web.Infrastructure.MediatR.Commands;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Handlers
{
    public sealed class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, Problem>
    {
        private readonly MainDbContext _db;

        public CreateProblemCommandHandler(MainDbContext db)
        {
            _db = db;
        }

        public async Task<Problem> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
        {
            var id = _db.Problems
                .OrderBy(x => x.Id)
                .LastOrDefault()?.Id + 1 ?? 1000;

            var classification = _db.ProblemClassifications
                .SingleOrDefault(x => x.Id == request.ClassificationId);
            var locale = _db.Locales
                .SingleOrDefault(x => x.Code == Locales.English);

            if (locale == null)
                throw new NullReferenceException();
            
            if (classification == null)
                throw new ArgumentNullException(nameof(classification));

            var datasets = new List<ProblemDataSet>();
            for (var i = 0; i < request.InputFiles.Count; i++)
            {
                var inputText = request.InputFiles[i]
                    .OpenReadStream()
                    .ReadToEnd();
                var outputText = request.OutputFiles[i]
                    .OpenReadStream()
                    .ReadToEnd();
                
                datasets.Add(new ProblemDataSet()
                {
                    Input = inputText,
                    Output = outputText,
                    
                });
            }

            var problemTranslation = new ProblemTranslation
            {
                Description = request.Description,
                Title = request.Title,
                Locale = locale,
            };

            var problem = new Problem
            {
                Id = id,
                Author = request.Author,
                Classification = classification,
                Enabled = true,
                Points = request.Points,
                Multidata = request.Multidata,
                MemoryLimit = request.MemoryLimit,
                OutputLimit = request.OutputLimit,
                SourceCodeLengthLimit = request.SourceCodeLengthLimit,
                SpecialJudge = request.SpecialJudge,
                TotalTimeLimit = request.TimeLimit,
                CaseTimeLimit = request.CaseTimeLimit,
                
                DataSets = datasets,
                Translations = new List<ProblemTranslation>
                {
                    problemTranslation
                }
            };

            _db.Problems.Add(problem);

            await _db.SaveChangesAsync(cancellationToken);

            return problem;
        }
    }
}