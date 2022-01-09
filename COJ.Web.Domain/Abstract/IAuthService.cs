using COJ.Web.Domain.Entities;
using COJ.Web.Domain.MediatR;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Models.Dtos;

namespace COJ.Web.Domain.Abstract;

public interface IAuthService
{
    Task<Account> SignUp(SignUpRequest request);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    /// <param name="argument"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException">When the credentials are wrong</exception>
    /// <exception cref="DisabledAccountException">When the account is disabled</exception>
    Task<Result<SignInResponse>> SignIn(SignInModel account, SignInArguments argument);

    Account RecoverAccount(string emailOrUsername);
    Task<RefreshTokenResult?> RefreshToken(string token);
}

