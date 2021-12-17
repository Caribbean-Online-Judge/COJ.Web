using COJ.Web.Domain.Values;

namespace COJ.Web.Domain.Entities;

public sealed class Problem : BaseEntity
{
    public Problem()
    {
        ProblemStatistics = new ProblemStatistic();
    }

    public string Author { get; set; }

    public double Points { get; set; }
    public int TotalTimeLimit { get; set; }
    public int MemoryLimit { get; set; }
    public int CaseTimeLimit { get; set; }
    public int OutputLimit { get; set; }
    public int SourceCodeLengthLimit { get; set; }
    public bool Multidata { get; set; }
    public bool SpecialJudge { get; set; }

    public bool? Enabled { get; set; }
    public bool Published { get; set; }

    public ProblemClassification Classification { get; set; }
    public ProblemStatistic ProblemStatistics { get; set; }
    public ICollection<ProblemDataSet> DataSets { get; set; }
    public ICollection<ProblemTranslation> Translations { get; set; }


    public string GetNeutralTitle()
    {
        return Translations
                   .SingleOrDefault(x => x.Locale.Code == Locales.English)?.Title
               ?? throw new InvalidOperationException();
    }

    public string GetLocalizedTitle(string culture)
    {
        return Translations
                   .SingleOrDefault(x => x.Locale.Code == culture)?.Title
               ?? string.Empty;
    }

    /// <summary>
    /// Check if exist translation for the provided locale
    /// </summary>
    /// <param name="locale">Locale to check</param>
    /// <returns></returns>
    public bool ExistTranslationFor(string locale)
    {
        return Translations.Any(x => x.Locale.Code == locale);
    }

    public string GetLocalizedDescription(string culture)
    {
        return Translations
                   .SingleOrDefault(x => x.Locale.Code == culture)?.Description
               ?? string.Empty;
    }
}