using BankApplication.Api.Controllers;
using BankApplication.Application.Binders;
using BankApplication.Core.Models;
using BankApplication.Infrastructure.Binders;
using NetArchTest.Rules;
using System.Reflection;

namespace BankApplication.Tests.ArchitectureTests;

public abstract class ArchitectureTestBase
{
    protected static readonly Assembly Domain = typeof(User).Assembly;
    protected static readonly Assembly Infrastructure = typeof(InfrastructureBinder).Assembly;
    protected static readonly Assembly Application = typeof(ApplicationBinder).Assembly;
    protected static readonly Assembly Api = typeof(UserController).Assembly;

    protected static readonly string NamespacePrefix = "BankApplication";
    
    protected static readonly List<string> AsyncWhiteListedMethodNames = new ()
    {
        "Invoke",
        "Handle"
    };

    protected IEnumerable<Assembly> GetAllAssemblies()
    {
        return new List<Assembly>
        {
            Domain,
            Application,
            Infrastructure,
            Api
        };
    }

    protected IEnumerable<Type> GetAllTypesFromAssemblies()
    {
        return Types.InAssemblies(GetAllAssemblies())
            .That()
            .ResideInNamespace(NamespacePrefix)
            .GetTypes();
    }
}