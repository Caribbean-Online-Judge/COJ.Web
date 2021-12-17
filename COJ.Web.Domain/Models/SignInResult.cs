using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Domain.Models
{
    public class SignInResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int ExpirationTime { get; set; }
    }
}
