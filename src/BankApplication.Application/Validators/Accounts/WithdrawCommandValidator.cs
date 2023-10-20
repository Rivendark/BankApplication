using BankApplication.Application.Commands.Accounts;
using FluentValidation;

namespace BankApplication.Application.Validators.Accounts;

public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawCommandValidator()
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
            .PrecisionScale(32, 2, true);
    }
}