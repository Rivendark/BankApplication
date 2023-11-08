using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class AsyncMethodParameterTests : ArchitectureTestBase
{
    [Test]
    public void AsyncMethods_Should_HaveCancellationTokenParameter()
    {
        var whiteListedBaseClasses = new List<Type>
        {
            typeof(ControllerBase),
            typeof(DbContext)
        };

        var types = GetAllTypesFromAssemblies();
        
        var failingTypes = new List<Type>();
        var failingMethods = new List<MethodInfo>();
        foreach (var entityType in types)
        {
            var methods = entityType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var asyncMethods = methods.Where(x => x.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null)
                .ToList();

            foreach (var asyncMethod
                     in from asyncMethod
                         in asyncMethods
                     where asyncMethod.DeclaringType == null
                           || !whiteListedBaseClasses.Contains(asyncMethod.DeclaringType)
                     let parameters = asyncMethod.GetParameters()
                     where parameters.All(p => p.ParameterType != typeof(CancellationToken))
                           && !AsyncWhiteListedMethodNames.Contains(asyncMethod.Name) 
                     select asyncMethod)
            {
                failingTypes.Add(entityType);
                failingMethods.Add(asyncMethod);
            }
        }
        
        Assert.Multiple(() =>
        {
            Assert.That(failingTypes, Is.Empty);
            Assert.That(failingMethods, Is.Empty);
        });
    }
}