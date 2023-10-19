using BankApplication.Application.Queries.Users;
using FluentValidation;

namespace BankApplication.Application.Validators.Users;

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(query => query.CorrelationId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}