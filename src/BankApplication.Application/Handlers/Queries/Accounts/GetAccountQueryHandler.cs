using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Queries.Accounts;
using BankApplication.Application.Repositories;
using BankApplication.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Queries.Accounts;

public sealed class GetAccountQueryHandler(IAccountRepository accountRepository, ILogger<GetAccountQueryHandler> logger)
    : IQueryHandler<GetAccountQuery, AccountDto>
{
    public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAccountAsync(request.AccountId, cancellationToken);

        if (account is not null)
        {
            return new AccountDto(account, request.CorrelationId);
        }

        var ex = new AccountNotFoundException();
        logger.LogInformation($"{GetType().Name}:{ex.Message}", request);
        throw ex;
    }
}