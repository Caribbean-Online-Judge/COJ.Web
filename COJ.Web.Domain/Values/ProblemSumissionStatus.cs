namespace COJ.Web.Domain.Values;

public enum SubmissionStatus
{
    /// <summary>
    /// When it is barely created.
    /// </summary>
    Created,
    /// <summary>
    /// When it's being judged.
    /// </summary>
    Judging,
    /// <summary>
    /// When it has been judged.
    /// </summary>
    Judged
}