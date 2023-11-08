using MediatR;
using NetArchTest.Rules;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class ApplicationEventsSealedTests : ArchitectureTestBase
{
    [Test]
    public void Events_Should_BeSealed()
    {
        var types = Types.InAssembly(Application)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IRequest<>))
            .Or()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(INotification));
            
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