namespace COJ.Web.Domain.Values;

public class Locales
{
    public const string English = "en";
    public const string Spanish = "es";

    public const string DefaultLocale = English;

    public static string[] SupportedLocales => new[]
    {
        English,
        Spanish
    };
}