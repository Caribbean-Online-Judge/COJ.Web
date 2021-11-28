using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;

namespace COJ.Web.Domain.Abstract;

public interface IAuthService
{
    Task<Account> SignUp(SignUpModel request);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    /// <param name="argument"></param>
    /// <returns></returns>
    /// <exception cref="NotAuthorizedException">When the credentials are wrong</exception>
    Task<SignInResult> SignIn(SignInModel account, SignInArguments argument);

    Account RecoverAccount(string emailOrUsername);
}

