using FluentValidation;
using BankApplication.Application.Behaviors;
using BankApplication.Application.Binders;
using BankApplication.Application.Middlewares;
using BankApplication.Infrastructure.Binders;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

ApplicationBinder.Bind(builder);
InfrastructureBinder.Bind(builder);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
});

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseMiddleware<UserExceptionHandlerMiddleware>();
app.UseMiddleware<AccountExceptionHandlerMiddleware>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();