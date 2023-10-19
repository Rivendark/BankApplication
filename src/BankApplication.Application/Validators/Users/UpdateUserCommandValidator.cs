using BankApplication.Application.Commands.Users;
using FluentValidation;

namespace BankApplication.Application.Validators.Users;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(cmd => cmd.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(cmd => cmd.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(cmd => cmd.FirstName)
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(255);
        
        RuleFor(cmd => cmd.LastName)
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(255);

        RuleFor(cmd => cmd.Email)
            .Length(5, 255)
            .When(cmd => !string.IsNullOrEmpty(cmd.Email))
            .WithMessage("Email must be between 5 and 255 characters.");
    }
}