using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;

namespace BankApplication.Application.Queries.Accounts;

public sealed class GetAccountsByUserIdQuery : IQuery<List<AccountDto>>
{
    public Guid CorrelationId { get; init; }
    public Guid UserId { get; init; }
}