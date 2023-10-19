using BankApplication.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BankApplication.Application.Middlewares;

public class UserExceptionHandlerMiddleware(RequestDelegate next) : BaseExceptionHandlerMiddleware(next)
{
    public override (HttpStatusCode? code, string message) GetResponse(Exception exception, HttpContext context)
    {
        HttpStatusCode? code;
        switch (exception)
        {
            case UserNotFoundException:
                code = HttpStatusCode.BadRequest;
                if (context.Request.Method == HttpMethod.Get.ToString())
                    code = HttpStatusCode.NotFound;
                break;
            
            case UserExistsException:
                code = HttpStatusCode.BadRequest;
                break;
            
            default:
                return (null, string.Empty);
        }
        
        return (code, exception.Message);
    }
}