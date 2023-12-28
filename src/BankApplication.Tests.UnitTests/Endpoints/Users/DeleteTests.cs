using BankApplication.Api.Endpoints.Users;
using BankApplication.Application.Commands.Users;
using BankApplication.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace BankApplication.Tests.UnitTests.Endpoints.Users;

[TestFixture]
public class DeleteTests
{
    private Mock<ISender> _senderMock;
    private Delete _endpoint;
    private CancellationTokenSource _cts;
    private static readonly Guid UserId = Guid.NewGuid();
    
    [SetUp]
    public void BeforeEachTest()
    {
        _cts = new CancellationTokenSource();
        _senderMock = new Mock<ISender>();
        _endpoint = new Delete(_senderMock.Object);
    }
    
    [Test]
    public async Task Should_Delete_User_If_Exists()
    {
        var cmd = new DeleteUserCommand()
        {
            CorrelationId = Guid.NewGuid(),
            UserId = UserId
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .Verifiable();
        
        var result = await _endpoint.HandleAsync(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<AcceptedResult>(), "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
        });
    }
    
    [Test]
    public void Delete_When_UserNotFound_Should_ThrowUserNotFoundException()
    {
        var cmd = new DeleteUserCommand()
        {
            CorrelationId = Guid.NewGuid(),
            UserId = UserId
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ThrowsAsync(new UserNotFoundException())
            .Verifiable();
        
        Assert.ThrowsAsync<UserNotFoundException>(async () => await _endpoint.HandleAsync(cmd, _cts.Token));
    }
}