using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Seeder.Utils
{
    internal class ConsoleUtils
    {
        public static void WriteLineWarning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(warning);

            Console.ResetColor();
        }

        internal static void WriteLineFatalError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(error);

            Console.ResetColor();
        }
    }
}
