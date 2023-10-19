using BankApplication.Application.Commands.Accounts;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using BankApplication.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Accounts;

public sealed class DeleteAccountCommandHandler(
        IAccountRepository accountRepository,
        IPublisher publisher,
        ILogger<DeleteAccountCommandHandler> logger)
    : ICommandHandler<DeleteAccountCommand>
{
    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await accountRepository.DeleteAccountAsync(request.AccountId, cancellationToken);
            await publisher.Publish(
                new AccountDeletedNotification(request.AccountId, request.CorrelationId),
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"{GetType()}:{ex.Message}", request);

            throw;
        }
    }
}