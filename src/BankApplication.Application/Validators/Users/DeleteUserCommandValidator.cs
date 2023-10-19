using BankApplication.Application.Commands.Users;
using FluentValidation;

namespace BankApplication.Application.Validators.Users;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(cmd => cmd.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}