using System.Reflection;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class AsyncMethodNamingConventionTests : ArchitectureTestBase
{
    [Test]
    public void AsyncMethods_Should_EndWithAsync()
    {
        var types = GetAllTypesFromAssemblies();

        var failingTypes = new List<Type>();
        var failingMethods = new List<MethodInfo>();
        foreach (var entityType in types)
        {
            if (entityType.Namespace != null && entityType.Namespace.Contains($"{NamespacePrefix}.Api.Controllers"))
            {
                continue;
            }
            
            var methods = entityType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var asyncMethods = methods.Where(x => x.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null)
                .ToList();

            foreach (var asyncMethod in asyncMethods.Where(
                         asyncMethod => !asyncMethod.Name.EndsWith("Async") 
                                        && !AsyncWhiteListedMethodNames.Contains(asyncMethod.Name)))
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