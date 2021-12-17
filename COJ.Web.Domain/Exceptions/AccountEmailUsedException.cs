using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ.Web.Domain.Exceptions
{
    [Serializable]
    public sealed class AccountEmailUsedException : Exception
    {
        public AccountEmailUsedException()
        {

        }
    }
}
