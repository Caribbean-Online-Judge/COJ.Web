using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;

namespace COJ.Web.Domain.Abstract;

public interface IAuthService
{
    Task<Account> SignUp(SignUpModel request);

    Account SignIn(Account account);

    Account RecoverAccount(string emailOrUsername);
}

