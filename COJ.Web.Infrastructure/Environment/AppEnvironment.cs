using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

using SystemEnvironment = System.Environment;

namespace COJ.Web.Infrastructure.Environment
{
    public sealed class AppEnvironment
    {
        public const string DATABASE_HOST_KEY = "DATABASE_HOST";
        public const string SMTP_HOST_KEY = "SMTP_HOST";
        public const string SMTP_PORT_KEY = "SMTP_PORT";

        public IConfiguration Configuration { get; }

        private readonly JwtSettings jwtSettings;

        public AppEnvironment(IConfiguration configuration)
        {
            Configuration = configuration;
            jwtSettings = new JwtSettings(configuration);
        }

        public string DatabaseConnectionString
        {
            get
            {
                var host = Configuration.GetValue<string>(DATABASE_HOST_KEY);
                var username = Configuration.GetValue<string>("DATABASE_USERNAME");
                var password = Configuration.GetValue<string>("DATABASE_PASSWORD");
                var name = Configuration.GetValue<string>("DATABASE_NAME");
                return $"Host={host};Username={username};Password={password};Database={name}";
            }
        }

        public JwtSettings Jwt => jwtSettings;

        public string SmtpHost => Configuration.GetValue<string>(SMTP_HOST_KEY);
        public int SmtpPort => Configuration.GetValue<int>(SMTP_PORT_KEY);

        public string FromMailAddress => Configuration.GetValue<string>("SMTP_FROM");

        public string SmtpUsername => Configuration.GetValue<string>("SMTP_USERNAME");
        public string SmtpPassword => Configuration.GetValue<string>("SMTP_PASSWORD");

        public static void LoadEnvFile()
        {
            var filePath = Path.Join(SystemEnvironment.CurrentDirectory, ".env");
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                SystemEnvironment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

    }

}
