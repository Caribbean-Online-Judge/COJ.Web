using Microsoft.Extensions.Configuration;

namespace COJ.Web.Infrastructure.Environment
{
    public sealed class JwtSettings
    {
        private const string EXPIRATION_TIME_KEY = "Auth:Jwt:ExpirationTime";
        private const string SECRET_KEY = "JWT_SECRET";

        internal JwtSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Expiration time in minutes.
        /// </summary>
        public int ExpirationTime => Configuration.GetValue<int>(EXPIRATION_TIME_KEY);
        public string Secret => Configuration.GetValue<string>(SECRET_KEY);
    }
}
