using MediatR;
using Microsoft.Extensions.Logging;

namespace BankApplication.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    (ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken) 
    {
        logger.LogInformation($"{GetType()}: Handling {typeof(TRequest).Name}");
        try
        {
            var response = await next();

            logger.LogInformation($"{GetType()}: Handled {typeof(TRequest).Name}. Response: {typeof(TResponse).Name}");

            return response;
        }
        catch (Exception ex)
        {
            logger.LogWarning($"{GetType()}: Handling {typeof(TRequest).Name} Failed. Message: {ex.Message}");
            throw;
        }
    }
}