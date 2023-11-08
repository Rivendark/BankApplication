using BankApplication.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BankApplication.Application.Binders;

public static class ApplicationBinder
{
    public static void Bind(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAccountLockService, AccountLockService>();
        builder.Services.AddTransient<IAccountValidationService, AccountValidationService>();
    }
}