using NetArchTest.Rules;
using BankApplication.Application.Mediatr;

namespace BankApplication.Tests.ArchitectureTests;

[TestFixture]
public class ClassNamingConventionTests : ArchitectureTestBase
{
    [Test]
    public void Commands_Should_HaveNameEndingWith_Command()
    {
        var types = Types.InAssemblies(GetAllAssemblies())
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Commands")
            .And()
            .ImplementInterface(typeof(ICommand<>))
            .Or()
            .ResideInNamespace($"{NamespacePrefix}.Application.Commands")
            .And()
            .ImplementInterface(typeof(ICommand));
            
        var result = types
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
    
    [Test]
    public void CommandHandlers_Should_HaveNameEndingWith_CommandHandler()
    {
        var types = Types.InCurrentDomain()
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Handlers.Commands")
            .And()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ResideInNamespace($"{NamespacePrefix}.Application.Handlers.Commands")
            .And()
            .ImplementInterface(typeof(ICommandHandler<,>));
            
        var result = types
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
    
    [Test]
    public void Queries_Should_HaveNameEndingWith_Query()
    {
        var types = Types.InCurrentDomain()
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Queries")
            .And()
            .ImplementInterface(typeof(IQuery<>))
            .Or()
            .ResideInNamespace($"{NamespacePrefix}.Application.Queries")
            .And()
            .ImplementInterface(typeof(IQuery));
            
        var result = types
            .Should()
            .HaveNameEndingWith("Query")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
    
    [Test]
    public void QueryHandlers_Should_HaveNameEndingWith_QueryHandler()
    {
        var types = Types.InCurrentDomain()
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Handlers.Queries")
            .And()
            .ImplementInterface(typeof(IQueryHandler<>))
            .Or()
            .ResideInNamespace($"{NamespacePrefix}.Application.Handlers.Queries")
            .And()
            .ImplementInterface(typeof(IQueryHandler<,>));
            
        var result = types
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
    
    [Test]
    public void Notifications_Should_HaveNameEndingWith_Notification()
    {
        var types = Types.InCurrentDomain()
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Notifications")
            .And()
            .ImplementInterface(typeof(IDomainNotification));
            
        var result = types
            .Should()
            .HaveNameEndingWith("Notification")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
    
    [Test]
    public void NotificationHandlers_Should_HaveNameEndingWith_NotificationHandler()
    {
        var types = Types.InCurrentDomain()
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Handlers.Notification")
            .And()
            .ImplementInterface(typeof(IDomainNotificationHandler<>));
            
        var result = types
            .Should()
            .HaveNameEndingWith("NotificationHandler")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
    
    [Test]
    public void Validators_Should_HaveNameEndingWith_Validator()
    {
        var types = Types.InCurrentDomain()
            .That()
            .ResideInNamespace($"{NamespacePrefix}.Application.Validators");
            
        var result = types
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.FailingTypes, Is.Null);
        });
    }
}