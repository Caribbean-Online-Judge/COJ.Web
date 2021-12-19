namespace COJ.Web.Infrastructure.Extensions;

public static class StreamExtensions
{
    public static string ReadToEnd(this Stream stream)
    {
        using var sr = new StreamReader(stream);
        return sr.ReadToEnd();
    }
}