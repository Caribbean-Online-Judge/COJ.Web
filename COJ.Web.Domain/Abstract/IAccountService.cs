using COJ.Web.Domain.Entities;
using COJ.Web.Domain.Models;

namespace COJ.Web.Domain.Abstract;

public interface IAccountService
{
    /// <summary>
    /// Confirm account.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<ServiceResponse<bool>> ConfirmAccount(ConfirmAccountRequest request);

    Task<Account> GetAccountById(int userId);
}