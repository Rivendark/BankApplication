using BankApplication.Application.Commands.Accounts;
using FluentValidation;

namespace BankApplication.Application.Validators.Accounts;

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(cmd => cmd.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.AccountId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}