using BankApplication.Api.Controllers;
using BankApplication.Application.Commands.Users;
using BankApplication.Application.DTOs;
using BankApplication.Application.Queries.Users;
using BankApplication.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace BankApplication.Tests.UnitTests.Controllers;

[TestFixture]
public class UserControllerTests
{
    private Mock<ISender> _senderMock;
    private UserController _controller;
    private CancellationTokenSource _cts;
    private static readonly Guid UserId = Guid.NewGuid();

    private UserDto _userDto = new ()
    {
        Email = "firstUser@test.com",
        FirstName = "first",
        LastName = "user",
        Id = UserId
    };

    [SetUp]
    public void BeforeEachTest()
    {
        _cts = new CancellationTokenSource();
        _senderMock = new Mock<ISender>();
        _controller = new UserController(_senderMock.Object);
    }

    [Test]
    public async Task Should_Create_User_And_Return_Http_Accepted()
    {
        var cmd = new CreateUserCommand
        {
            CorrelationId = Guid.NewGuid(),
            Email = _userDto.Email,
            FirstName = _userDto.FirstName,
            LastName = _userDto.LastName,
            SendingSystemId = Guid.NewGuid()
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ReturnsAsync(_userDto)
            .Verifiable();
        
        var result = await _controller.Create(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<AcceptedResult>(), "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
            Assert.That(acceptedResult.Value, Is.Not.Null);
            Assert.That(acceptedResult.Value, Is.InstanceOf<UserDto>(), "acceptedResult.Value not instance of UserDto.");
            var returnedUserDto = (UserDto)acceptedResult.Value!;
            Assert.That(returnedUserDto.FirstName, Is.EqualTo(_userDto.FirstName), "First names are not equal.");
        });
    }

    [Test]
    public async Task Should_Return_BadRequest_On_Create_If_User_Id_Exists()
    {
        var cmd = new CreateUserCommand
        {
            CorrelationId = Guid.NewGuid(),
            Email = _userDto.Email,
            FirstName = _userDto.FirstName,
            LastName = _userDto.LastName,
            SendingSystemId = Guid.NewGuid()
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ThrowsAsync(new UserExistsException())
            .Verifiable();
        
        var result = await _controller.Create(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>(), "Result not instance of BadRequestObjectResult.");
            var acceptedResult = (BadRequestObjectResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest), "Http Status Code is incorrect.");
        });
    }
    
    [Test]
    public async Task Should_Update_User_If_Exists()
    {
        var cmd = new UpdateUserCommand()
        {
            CorrelationId = Guid.NewGuid(),
            UserId = UserId,
            Email = _userDto.Email,
            FirstName = _userDto.FirstName,
            LastName = _userDto.LastName,
            SendingSystemId = Guid.NewGuid()
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ReturnsAsync(_userDto)
            .Verifiable();
        
        var result = await _controller.Update(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<AcceptedResult>(), "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
            Assert.That(acceptedResult.Value, Is.Not.Null);
            Assert.That(acceptedResult.Value, Is.InstanceOf<UserDto>(), "acceptedResult.Value not instance of UserDto.");
            var returnedUserDto = (UserDto)acceptedResult.Value!;
            Assert.That(returnedUserDto.FirstName, Is.EqualTo(_userDto.FirstName), "First names are not equal.");
        });
    }
    
    [Test]
    public async Task Should_Return_BadRequest_On_Update_If_User_Does_Not_Exist()
    {
        var cmd = new UpdateUserCommand
        {
            CorrelationId = Guid.NewGuid(),
            Email = _userDto.Email,
            FirstName = _userDto.FirstName,
            LastName = _userDto.LastName,
            SendingSystemId = Guid.NewGuid()
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ThrowsAsync(new UserNotFoundException())
            .Verifiable();
        
        var result = await _controller.Update(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>(), "Result not instance of BadRequestResult.");
            var acceptedResult = (BadRequestObjectResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest), "Http Status Code is incorrect.");
        });
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
        
        var result = await _controller.Delete(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<AcceptedResult>(), "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
        });
    }
    
    [Test]
    public async Task Should_Return_BadRequest_On_Delete_If_User_Does_Not_Exist()
    {
        var cmd = new DeleteUserCommand()
        {
            CorrelationId = Guid.NewGuid(),
            UserId = UserId
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ThrowsAsync(new UserNotFoundException())
            .Verifiable();
        
        var result = await _controller.Delete(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>(), "Result not instance of BadRequestResult.");
            var acceptedResult = (BadRequestObjectResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest), "Http Status Code is incorrect.");
        });
    }
    
    [Test]
    public async Task Should_Return_Select_Users_List()
    {
        var cmd = new GetUsersQuery
        {
            CorrelationId = Guid.NewGuid()
        };
        
        _senderMock.Setup(x => x.Send(cmd, _cts.Token))
            .ReturnsAsync(new List<UserDto>
            {
                _userDto
            })
            .Verifiable();
        
        var result = await _controller.GetSelectUsers(cmd, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value!, Is.InstanceOf<List<UserDto>>());
            var userList = (List<UserDto>) okObjectResult.Value!;
            Assert.That(userList, Has.Count.EqualTo(1));
        });
    }
    
    [Test]
    public async Task Should_Return_All_Users_List()
    {
        _senderMock.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), _cts.Token))
            .ReturnsAsync(new List<UserDto>
            {
                _userDto
            })
            .Verifiable();
        
        var result = await _controller.GetUsers(_cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value!, Is.InstanceOf<List<UserDto>>());
            var userList = (List<UserDto>) okObjectResult.Value!;
            Assert.That(userList, Has.Count.EqualTo(1));
        });
    }
    
    [Test]
    public async Task Should_Return_User_If_Exists()
    {
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserQuery>(), _cts.Token))
            .ReturnsAsync(_userDto)
            .Verifiable();
        
        var result = await _controller.GetUser(UserId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.Value, Is.Not.Null);
            Assert.That(okObjectResult.Value, Is.InstanceOf<UserDto>());
            var userResult = (UserDto)okObjectResult.Value!;
            Assert.That(_userDto.Id, Is.EqualTo(userResult.Id));
        });
    }
    
    [Test]
    public async Task Should_Return_Null_On_GetUser_If_User_Not_Found()
    {
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserQuery>(), _cts.Token))
            .ThrowsAsync(new UserNotFoundException())
            .Verifiable();
        
        var result = await _controller.GetUser(UserId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _senderMock.Verify();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var notFoundObjectResult = (NotFoundObjectResult)result;
            Assert.That(notFoundObjectResult.Value, Is.Not.Null);
            Assert.That(notFoundObjectResult.Value, Is.InstanceOf<Guid>());
            var guidResponse = (Guid)notFoundObjectResult.Value!;
            Assert.That(UserId, Is.EqualTo(guidResponse));
        });
    }
    
    [TearDown]
    public void AfterEachTest()
    {
        _cts.Dispose();
    }
}