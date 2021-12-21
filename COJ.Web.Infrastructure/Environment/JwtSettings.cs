using Microsoft.Extensions.Configuration;

namespace COJ.Web.Infrastructure.Environment
{
    public sealed class JwtSettings
    {
        public const string EXPIRATION_TIME_KEY = "Auth:Jwt:ExpirationTime";
        public const string VALIDATE_ISSUER_KEY = "Auth:Jwt:ValidateIssuer";
        public const string VALIDATE_AUDIENCE_KEY = "Auth:Jwt:ValidateAudience";
        public const string VALID_ISSUER_KEY = "Auth:Jwt:ValidIssuers";
        public const string VALID_AUDIENCE_KEY = "Auth:Jwt:ValidAudiences";
        public const string SECRET_KEY = "JWT_SECRET";

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
        public bool ValidateIssuer => Configuration.GetValue(VALIDATE_ISSUER_KEY, true);
        public bool ValidateAudience => Configuration.GetValue(VALIDATE_ISSUER_KEY, true);
        public IEnumerable<string> ValidIssuers => Configuration.GetValue<string[]>(VALID_ISSUER_KEY);
        public IEnumerable<string> ValidAudiences => Configuration.GetValue<string[]>(VALID_AUDIENCE_KEY);
    }
}
