using Microsoft.EntityFrameworkCore;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Infrastructure.Contexts;
using RadancyBankApplication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseInMemoryDatabase("bankDb"));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();