using NetArchTest.Rules;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class DomainModelsSealedTests : ArchitectureTestBase
{
    [Test]
    public void Models_Should_BeSealed()
    {
        var types = Types.InAssembly(Domain)
            .That()
            .ResideInNamespace("BankApplication.Core.Models")
            .Or()
            .ResideInNamespace("BankApplication.Core.Exceptions");

        var result = types
            .Should()
            .BeSealed()
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
}