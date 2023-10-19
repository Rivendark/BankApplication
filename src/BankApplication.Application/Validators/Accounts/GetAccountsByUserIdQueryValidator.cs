using BankApplication.Application.Queries.Accounts;
using FluentValidation;

namespace BankApplication.Application.Validators.Accounts;

public class GetAccountsByUserIdQueryValidator : AbstractValidator<GetAccountsByUserIdQuery>
{
    public GetAccountsByUserIdQueryValidator()
    {
        RuleFor(query => query.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(query => query.UserId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}