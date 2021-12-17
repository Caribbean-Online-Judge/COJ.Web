using COJ.Web.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Domain.Abstract
{
    public interface IJwtService
    {
        /// <summary>
        /// Create a token from a provided account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="expirationTime">Expiration time for the token</param>
        /// <returns></returns>
        string ComputeToken(Account account, out int expirationTime);

        object DecodeToken(string token);
    }
}
