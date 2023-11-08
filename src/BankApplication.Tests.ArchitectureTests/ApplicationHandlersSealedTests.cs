using MediatR;
using NetArchTest.Rules;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class ApplicationHandlersSealedTests : ArchitectureTestBase
{
    [Test]
    public void EventHandlers_Should_BeSealed()
    {
        var types = Types.InAssembly(Application)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Or()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(INotificationHandler<>));
            
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