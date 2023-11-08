using BankApplication.Application.Commands.Accounts;
using BankApplication.Application.DTOs;
using BankApplication.Application.Mediatr;
using BankApplication.Application.Notifications.Accounts;
using BankApplication.Application.Repositories;
using BankApplication.Application.Services;
using BankApplication.Core.Enums;
using BankApplication.Core.Exceptions;
using BankApplication.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Handlers.Commands.Accounts;

public sealed class DepositCommandHandler(
        IAccountRepository accountRepository,
        IPublisher publisher,
        ILogger<DepositCommandHandler> logger,
        IAccountValidationService validationService,
        IAccountLockService accountLockService)
    : ICommandHandler<DepositCommand, AccountDto>
{
    public async Task<AccountDto> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await accountRepository.GetAccountAsync(request.AccountId, cancellationToken);
            if (account is null || account.UserId != request.UserId)
            {
                throw new AccountNotFoundException();
            }

            if (!await accountLockService.TryGetLockAsync(request.AccountId, cancellationToken))
            {
                logger.LogWarning($"Failed to secure lock on account. AccountId: {request.AccountId}");
                throw new FailedToAchieveAccountLockException();
            }
            
            logger.LogInformation($"Acquired lock on account. AccountId: {request.AccountId}");

            var balanceChange = new BalanceChange
            {
                AccountId = account.Id,
                Amount = request.Amount,
                CreatedAtUtc = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Type = BalanceChangeType.Deposit,
                UserId = request.UserId
            };

            validationService.ValidateDeposit(account, balanceChange);

            account.ApplyBalanceChange(balanceChange);
            account = await accountRepository.UpdateAccountBalanceAsync(account, balanceChange, cancellationToken);

            await publisher.Publish(
                new AccountBalanceChangedNotification(balanceChange, request.CorrelationId),
                cancellationToken);

            return new AccountDto(account, request.CorrelationId);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"{GetType().Name}:{ex.Message}", request);

            throw;
        }
        finally
        {
            await accountLockService.ReleaseLockAsync(cancellationToken);
            logger.LogInformation($"Released lock on account. AccountId: {request.AccountId}");
        }
    }
}