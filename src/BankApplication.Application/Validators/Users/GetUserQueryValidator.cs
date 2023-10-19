using BankApplication.Application.Queries.Users;
using FluentValidation;

namespace BankApplication.Application.Validators.Users;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(query => query.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(query => query.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}