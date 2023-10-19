using BankApplication.Application.Queries.Accounts;
using FluentValidation;

namespace BankApplication.Application.Validators.Accounts;

public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
{
    public GetAccountQueryValidator()
    {
        RuleFor(query => query.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(query => query.AccountId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}