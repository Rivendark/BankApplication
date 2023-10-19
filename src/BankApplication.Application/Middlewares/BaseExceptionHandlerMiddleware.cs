using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Reflection;

namespace BankApplication.Application.Middlewares;

public abstract class BaseExceptionHandlerMiddleware(RequestDelegate next)
{
    private static readonly ILogger Logger = Log.ForContext(MethodBase.GetCurrentMethod()?.DeclaringType);

    public abstract (HttpStatusCode? code, string message) GetResponse(Exception exception, HttpContext context);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            // log the error
            Logger.Error(exception, "error during executing {Context}", context.Request.Path.Value);
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