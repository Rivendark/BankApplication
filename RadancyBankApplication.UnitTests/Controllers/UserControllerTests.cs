using Microsoft.AspNetCore.Mvc;
using Moq;
using RadancyBankApplication.Api.Controllers;
using RadancyBankApplication.Api.DTOs;
using RadancyBankApplication.Application.Repositories;
using RadancyBankApplication.Core.Exceptions;
using RadancyBankApplication.Core.Models;
using System.Net;

namespace RadancyBankApplication.UnitTests.Controllers;

[TestFixture]
public class UserControllerTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IAccountRepository> _accountRepositoryMock;
    private UserController _controller;
    private CancellationTokenSource _cts;
    private static readonly Guid _userId = Guid.NewGuid();

    private UserDto _userDto = new UserDto
    {
        Email = "firstUser@test.com",
        FirstName = "first",
        LastName = "user",
        Id = _userId
    };

    [SetUp]
    public void BeforeEachTest()
    {
        _cts = new CancellationTokenSource();
        _userRepositoryMock = new Mock<IUserRepository>();
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _controller = new UserController(_userRepositoryMock.Object, _accountRepositoryMock.Object);
    }

    [Test]
    public async Task Should_Create_User_And_Return_Http_Accepted()
    {
        _userRepositoryMock.Setup(x => x.CreateUserAsync(It.Is<User>(u => u.Id == _userId), _cts.Token))
            .Verifiable();
        
        var result = await _controller.Create(_userDto, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.IsInstanceOf<AcceptedResult>(result, "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
            Assert.IsInstanceOf<UserDto>(acceptedResult.Value, "acceptedResult.Value not instance of UserDto.");
            var returnedUserDto = (UserDto)acceptedResult.Value;
            Assert.That(returnedUserDto.FirstName, Is.EqualTo(_userDto.FirstName), "First names are not equal.");
        });
    }

    [Test]
    public async Task Should_Return_BadRequest_On_Create_If_User_Id_Exists()
    {
        _userRepositoryMock.Setup(x => x.CreateUserAsync(It.Is<User>(u => u.Id != Guid.NewGuid()), _cts.Token))
            .ThrowsAsync(new UserExistsException())
            .Verifiable();
        
        var result = await _controller.Create(_userDto, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "Result not instance of BadRequestObjectResult.");
            var acceptedResult = (BadRequestObjectResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest), "Http Status Code is incorrect.");
        });
    }

    [Test]
    public async Task Should_Update_User_If_Exists()
    {
        _userRepositoryMock.Setup(x => x.UpdateUserAsync(It.Is<User>(u => u.Id == _userId), _cts.Token))
            .Verifiable();

        var result = await _controller.Update(_userDto, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.IsInstanceOf<AcceptedResult>(result, "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
            Assert.IsInstanceOf<UserDto>(acceptedResult.Value, "acceptedResult.Value not instance of UserDto.");
            var returnedUserDto = (UserDto)acceptedResult.Value;
            Assert.That(returnedUserDto.FirstName, Is.EqualTo(_userDto.FirstName), "First names are not equal.");
        });
    }
    
    [Test]
    public async Task Should_Return_BadRequest_On_Update_If_User_Does_Not_Exist()
    {
        _userRepositoryMock.Setup(x => x.UpdateUserAsync(It.Is<User>(u => u.Id != Guid.NewGuid()), _cts.Token))
            .ThrowsAsync(new UserNotFoundException())
            .Verifiable();
        
        var result = await _controller.Update(_userDto, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "Result not instance of BadRequestResult.");
            var acceptedResult = (BadRequestObjectResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest), "Http Status Code is incorrect.");
        });
    }
    
    [Test]
    public async Task Should_Delete_User_If_Exists()
    {
        _userRepositoryMock.Setup(x => x.DeleteUserAsync(_userId, _cts.Token))
            .Verifiable();

        var result = await _controller.Delete(_userId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.IsInstanceOf<AcceptedResult>(result, "Result not instance of AcceptedResult.");
            var acceptedResult = (AcceptedResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Accepted), "Http Status Code is incorrect.");
            Assert.IsInstanceOf<Guid>(acceptedResult.Value, "acceptedResult.Value not instance of UserDto.");
            var returnedUserId = (Guid)acceptedResult.Value;
            Assert.That(_userId, Is.EqualTo(returnedUserId), "Returned UserId is incorrect.");
        });
    }
    
    [Test]
    public async Task Should_Return_BadRequest_On_Delete_If_User_Does_Not_Exist()
    {
        _userRepositoryMock.Setup(x => x.DeleteUserAsync(It.Is<Guid>(u => u != Guid.NewGuid()), _cts.Token))
            .ThrowsAsync(new UserNotFoundException())
            .Verifiable();
        
        var result = await _controller.Delete(_userId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.IsInstanceOf<BadRequestObjectResult>(result, "Result not instance of BadRequestResult.");
            var acceptedResult = (BadRequestObjectResult)result;
            Assert.That(acceptedResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest), "Http Status Code is incorrect.");
        });
    }

    [Test]
    public async Task Should_Return_Users_List()
    {
        _userRepositoryMock.Setup(x => x.GetUsersAsync(_cts.Token))
            .ReturnsAsync(new List<User>
            {
                _userDto.ToDomainModel()
            })
            .Verifiable();
        
        var result = await _controller.GetUsers(_cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.IsInstanceOf<List<UserDto>>(okObjectResult.Value);
            var userList = (List<UserDto>) okObjectResult.Value;
            Assert.That(1, Is.EqualTo(userList.Count));
        });
    }

    [Test]
    public async Task Should_Return_User_If_Exists()
    {
        _userRepositoryMock.Setup(x => x.GetUserAsync(_userId, _cts.Token))
            .ReturnsAsync(_userDto.ToDomainModel())
            .Verifiable();
        
        var result = await _controller.GetUser(_userId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.IsInstanceOf<UserDto>(okObjectResult.Value);
            var userResult = (UserDto)okObjectResult.Value;
            Assert.That(_userDto.Id, Is.EqualTo(userResult.Id));
        });
    }

    [Test]
    public async Task Should_Return_Null_On_GetUser_If_User_Not_Found()
    {
        _userRepositoryMock.Setup(x => x.GetUserAsync(_userId, _cts.Token))
            .ReturnsAsync((User) null)
            .Verifiable();
        
        var result = await _controller.GetUser(_userId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            _userRepositoryMock.Verify();
            Assert.NotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundObjectResult = (NotFoundObjectResult)result;
            Assert.IsInstanceOf<Guid>(notFoundObjectResult.Value);
            var guidResponse = (Guid)notFoundObjectResult.Value;
            Assert.That(_userId, Is.EqualTo(guidResponse));
        });
    }
}