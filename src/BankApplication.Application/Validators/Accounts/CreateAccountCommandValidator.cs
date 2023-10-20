using BankApplication.Application.Commands.Accounts;
using FluentValidation;

namespace BankApplication.Application.Validators.Accounts;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(cmd => cmd.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.Name)
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(255);
    }
}