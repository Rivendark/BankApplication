using NetArchTest.Rules;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class DependencyTests : ArchitectureTestBase
{
    [Test]
    public void Domain_Should_HaveNoDependencies()
    {
        var infrastructureResult = Types.InAssembly(Domain)
            .Should()
            .NotHaveDependencyOn("BankApplication.Infrastructure")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(infrastructureResult.IsSuccessful, Is.True);
            Assert.That(infrastructureResult.FailingTypes, Is.Null);
        });
        
        var applicationResult = Types.InAssembly(Domain)
            .Should()
            .NotHaveDependencyOn("BankApplication.Application")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(applicationResult.IsSuccessful, Is.True);
            Assert.That(applicationResult.FailingTypes, Is.Null);
        });
        
        var apiResult = Types.InAssembly(Domain)
            .Should()
            .NotHaveDependencyOn("BankApplication.Api")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(apiResult.IsSuccessful, Is.True);
            Assert.That(apiResult.FailingTypes, Is.Null);
        });
    }

    [Test]
    public void Application_Should_NotDependOnInfrastructureOrApi()
    {
        var infrastructureResult = Types.InAssembly(Application)
            .Should()
            .NotHaveDependencyOn("BankApplication.Infrastructure")
            .GetResult();

        Assert.Multiple(() =>
        {
            Assert.That(infrastructureResult.IsSuccessful, Is.True);
            Assert.That(infrastructureResult.FailingTypes, Is.Null);
        });
        
        var apiResult = Types.InAssembly(Application)
            .Should()
            .NotHaveDependencyOn("BankApplication.Api")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(apiResult.IsSuccessful, Is.True);
            Assert.That(apiResult.FailingTypes, Is.Null);
        });
    }

    [Test]
    public void Infrastructure_Should_NotDependOnApi()
    {
        var apiResult = Types.InAssembly(Infrastructure)
            .Should()
            .NotHaveDependencyOn("BankApplication.Api")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(apiResult.IsSuccessful, Is.True);
            Assert.That(apiResult.FailingTypes, Is.Null);
        });
    }
}