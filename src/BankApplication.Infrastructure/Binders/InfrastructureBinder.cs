using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BankApplication.Application.Repositories;
using BankApplication.Infrastructure.Contexts;
using BankApplication.Infrastructure.Repositories;

namespace BankApplication.Infrastructure.Binders;

public static class InfrastructureBinder
{
    public static void Bind(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<BankDbContext>(options =>
            options.UseInMemoryDatabase("bankDb"));
        
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
    }
}