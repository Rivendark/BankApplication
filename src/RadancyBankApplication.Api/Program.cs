using Microsoft.EntityFrameworkCore;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Application.Services;
using RadancyBankApplication.Infrastructure.Contexts;
using RadancyBankApplication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseInMemoryDatabase("bankDb"));

builder.Services.AddTransient<IAccountValidationService, AccountValidationService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();