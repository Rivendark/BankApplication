using BankApplication.Application.Commands.Accounts;
using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using BankApplication.Application.Repositories;
using BankApplication.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Accounts;

public sealed class UpdateAccountCommandHandler(
        IAccountRepository accountRepository,
        IPublisher publisher,
        ILogger<UpdateAccountCommandHandler> logger)
    : ICommandHandler<UpdateAccountCommand, AccountDto>
{
    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await accountRepository.UpdateAccountInformationAsync(new Account
            {
                Id = request.AccountId,
                Name = request.AccountName
            }, cancellationToken);

            await publisher.Publish(new AccountUpdatedNotification(account, request.CorrelationId), cancellationToken);

            return new AccountDto(account, request.CorrelationId);
        }
        catch (Exception ex)
        { 
            logger.LogInformation($"{GetType()}:{ex.Message}", request);

            throw;
        }
    }
}