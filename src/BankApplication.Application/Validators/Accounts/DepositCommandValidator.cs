﻿using BankApplication.Application.Commands.Accounts;
using FluentValidation;

namespace BankApplication.Application.Validators.Accounts;

public class DepositCommandValidator : AbstractValidator<DepositCommand>
{
    public DepositCommandValidator()
    {
        RuleFor(cmd => cmd.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.AccountId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(cmd => cmd.Amount)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo((decimal)0.01)
            .PrecisionScale(2, 32, true);

    }
}