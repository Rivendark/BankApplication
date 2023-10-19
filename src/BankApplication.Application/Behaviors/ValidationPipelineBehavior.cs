﻿using FluentValidation;
using MediatR;

namespace BankApplication.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if(failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw;
        }
    }
}