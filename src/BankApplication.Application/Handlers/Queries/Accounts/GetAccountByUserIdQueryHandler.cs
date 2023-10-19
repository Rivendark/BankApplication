using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Queries.Accounts;
using BankApplication.Application.Repositories;

namespace BankApplication.Application.Handlers.Queries.Accounts;

public sealed class GetAccountByUserIdQueryHandler(IAccountRepository accountRepository)
    : IQueryHandler<GetAccountsByUserIdQuery, List<AccountDto>>
{
    public async Task<List<AccountDto>> Handle(GetAccountsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.GetUserAccountsAsync(request.UserId, cancellationToken);
        return accounts.Count != 0
            ? accounts.Select(x => new AccountDto(x, request.CorrelationId)).ToList()
            : new List<AccountDto>();
    }
}