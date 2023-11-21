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
        var keepAliveConnection = new SqliteConnection("DataSource=bankdb;mode=memory;cache=shared");
        keepAliveConnection.Open();
        
        builder.Services.AddDbContext<BankDbContext>(options =>
            options.UseSqlite(keepAliveConnection));
        
        var contextServiceProvider = builder.Services.BuildServiceProvider();
        using (var scope = contextServiceProvider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;

            using (var myDb = scopedProvider.GetRequiredService<BankDbContext>())
            {
                var result = myDb.Database.EnsureCreated();
                Console.WriteLine($"DB created: {result}");
            }
        }
        
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddTransient<IAccountLockRepository, AccountLockRepository>();
    }
}