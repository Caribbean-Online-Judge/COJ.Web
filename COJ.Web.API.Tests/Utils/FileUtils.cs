using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace COJ.Web.API.Tests.Utils;

public static class FileUtils
{
    public static void LogInternalServerError(string error)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Logs");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        File.WriteAllText(Path.Combine(path, $"Internal Server Error {DateTime.Now.ToShortTimeString()}.txt"), error, Encoding.UTF8);
    }
}