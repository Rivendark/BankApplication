using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Queries.Accounts;

public sealed class GetAccountQuery : IQuery<AccountDto>
{
    public Guid CorrelationId { get; init; }
    public Guid AccountId { get; init; }
}