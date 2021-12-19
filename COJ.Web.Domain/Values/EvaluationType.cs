namespace COJ.Web.Domain.Values;

/// <summary>
/// Evaluations types
/// </summary>
public enum EvaluationType
{
    /// <summary>
    /// Normal judge. It's the default judge type.
    /// </summary>
    NormalJudge,

    /// <summary>
    /// Special judge type. It's used when the submission use it own judging solution.
    /// </summary>
    SpecialJudge
}