using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using COJ.Web.Infrastructure.Environment;

namespace COJ.Web.Infrastructure.Services
{
    public sealed class JwtService : IJwtService
    {
        private readonly AppEnvironment _appEnvironment;

        #region Constructor
        public JwtService(AppEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        #endregion

        public string ComputeToken(Account account, out int expirationTime)
        {
            if (account.Permissions == null)
                throw new ArgumentNullException(nameof(account));

            var roles = string.Join(",", account.Permissions.Select(x => x.Permission).ToArray());
            
            var symmetricKey = Encoding.UTF8.GetBytes(_appEnvironment.Jwt.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, roles),
                    new Claim(ClaimTypes.NameIdentifier, $"{account.Id}")
                }),

                Expires = now.AddMinutes(_appEnvironment.Jwt.ExpirationTime),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            expirationTime = _appEnvironment.Jwt.ExpirationTime;

            return token;
        }

        public object DecodeToken(string token)
        {
            var isValidToken = ValidateToken(token, out var claims);

            return new { };
        }

        private bool ValidateToken(string token, out Claim[]? claims)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    claims = null;
                    return false;
                }

                var symmetricKey = Convert.FromBase64String(_appEnvironment.Jwt.Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

                claims = principal.Claims.ToArray();
                return true;
            }
            catch (Exception)
            {
                claims = null;
                return false;
            }
        }

    }
}
