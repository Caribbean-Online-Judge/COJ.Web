using COJ.Web.Domain.Entities;
using COJ.Web.Infrestructure.Data;
using MediatR;

namespace COJ.Web.Infrastructure.MediatR.Queries;
public class GetAccountByEmailQuery : IRequest<Account>
{
    public string Email { get; set; }
}

public class GetCustomerByIdQueryHandler : IRequestHandler<GetAccountByEmailQuery, Account?>
{
    private readonly MainDbContext db;

    public GetCustomerByIdQueryHandler(MainDbContext db)
    {
        this.db = db;
    }

    public async Task<Account?> Handle(GetAccountByEmailQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => db.Accounts.SingleOrDefault(account => account.Email == request.Email));
    }
}

