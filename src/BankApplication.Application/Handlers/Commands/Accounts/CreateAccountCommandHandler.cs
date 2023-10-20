using BankApplication.Application.Commands.Accounts;
using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Accounts;

public sealed class CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        IPublisher publisher,
        ILogger<CreateAccountCommandHandler> logger)
    : ICommandHandler<CreateAccountCommand, AccountDto>
{
    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await accountRepository.CreateAccountAsync(new Account
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name
            }, cancellationToken);

            await publisher.Publish(new AccountCreatedNotification(account, request.CorrelationId), cancellationToken);

            return new AccountDto(account, request.CorrelationId);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"{GetType().Name}:{ex.Message}", request);

            throw;
        }
    }
}