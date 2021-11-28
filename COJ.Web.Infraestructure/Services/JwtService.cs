using COJ.Web.Domain.Abstract;
using COJ.Web.Domain.Entities;
using COJ.Web.Infraestructure.Environment;

using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Infraestructure.Services
{
    public sealed class JwtService : IJwtService
    {
        public JwtService(AppEnvironment appEnvironment)
        {
            AppEnvironment = appEnvironment;
        }

        public AppEnvironment AppEnvironment { get; }

        public string ComputeToken(Account account, out int expirationTime)
        {
            var symmetricKey = Encoding.UTF8.GetBytes(AppEnvironment.Jwt.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, account.Role.HasValue ? account.Role?.ToString() : string.Empty),
                    //TODO: Include Roles
                }),

                Expires = now.AddMinutes(AppEnvironment.Jwt.ExpirationTime),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            expirationTime = AppEnvironment.Jwt.ExpirationTime;

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

                var symmetricKey = Convert.FromBase64String(AppEnvironment.Jwt.Secret);

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
