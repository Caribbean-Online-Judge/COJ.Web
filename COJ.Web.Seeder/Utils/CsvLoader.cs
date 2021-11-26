using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Seeder.Utils
{
    internal class CsvLoader
    {
        public static List<string[]> Load(string filePath)
        {
            var result = new List<string[]>();

            var file = File.Open(filePath, FileMode.Open, FileAccess.Read);
            TextReader reader = new StreamReader(file);
            string? line = string.Empty;

            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    if (line.StartsWith("#"))
                        continue;
                    result.Add(line.Split(new char[] { ',' }, StringSplitOptions.None));
                }

            } while (line != null);

            return result;

        }
    }
}
