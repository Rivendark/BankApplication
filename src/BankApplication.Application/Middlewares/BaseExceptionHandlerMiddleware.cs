using Microsoft.AspNetCore.Http;
using System.Net;

namespace BankApplication.Application.Middlewares;

public abstract class BaseExceptionHandlerMiddleware(RequestDelegate next)
{
    public abstract (HttpStatusCode? code, string message) GetResponse(Exception exception, HttpContext context);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            // get the response code and message
            var (status, message) = GetResponse(exception, context);

            if (status is null)
            {
                throw;
            }
            
            response.StatusCode = (int) status;
            await response.WriteAsync(message);
        }
    }
}