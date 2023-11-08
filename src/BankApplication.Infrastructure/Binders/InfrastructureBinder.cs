using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BankApplication.Application.Repositories;
using BankApplication.Infrastructure.Contexts;
using BankApplication.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;

namespace BankApplication.Infrastructure.Binders;

public static class InfrastructureBinder
{
    public static void Bind(WebApplicationBuilder builder)
    {
        var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        keepAliveConnection.Open();
        
        builder.Services.AddDbContext<BankDbContext>(options =>
            options.UseSqlite(keepAliveConnection));
        
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddTransient<IAccountLockRepository, AccountLockRepository>();
    }
}