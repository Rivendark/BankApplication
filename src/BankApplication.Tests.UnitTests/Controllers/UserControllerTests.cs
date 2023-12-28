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
    
    //
    //
    // [Test]
    // public async Task Should_Return_Select_Users_List()
    // {
    //     var cmd = new GetUsersQuery
    //     {
    //         CorrelationId = Guid.NewGuid()
    //     };
    //     
    //     _senderMock.Setup(x => x.Send(cmd, _cts.Token))
    //         .ReturnsAsync(new List<UserDto>
    //         {
    //             _userDto
    //         })
    //         .Verifiable();
    //     
    //     var result = await _controller.GetSelectUsers(cmd, _cts.Token);
    //     
    //     Assert.Multiple(() =>
    //     {
    //         _senderMock.Verify();
    //         Assert.That(result, Is.Not.Null);
    //         Assert.That(result, Is.InstanceOf<OkObjectResult>());
    //         var okObjectResult = (OkObjectResult)result;
    //         Assert.That(okObjectResult.Value, Is.Not.Null);
    //         Assert.That(okObjectResult.Value!, Is.InstanceOf<List<UserDto>>());
    //         var userList = (List<UserDto>) okObjectResult.Value!;
    //         Assert.That(userList, Has.Count.EqualTo(1));
    //     });
    // }
    //
    // [Test]
    // public async Task Should_Return_All_Users_List()
    // {
    //     _senderMock.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), _cts.Token))
    //         .ReturnsAsync(new List<UserDto>
    //         {
    //             _userDto
    //         })
    //         .Verifiable();
    //     
    //     var result = await _controller.GetUsers(_cts.Token);
    //     
    //     Assert.Multiple(() =>
    //     {
    //         _senderMock.Verify();
    //         Assert.That(result, Is.Not.Null);
    //         Assert.That(result, Is.InstanceOf<OkObjectResult>());
    //         var okObjectResult = (OkObjectResult)result;
    //         Assert.That(okObjectResult.Value, Is.Not.Null);
    //         Assert.That(okObjectResult.Value!, Is.InstanceOf<List<UserDto>>());
    //         var userList = (List<UserDto>) okObjectResult.Value!;
    //         Assert.That(userList, Has.Count.EqualTo(1));
    //     });
    // }
    //
    // [Test]
    // public async Task Should_Return_User_If_Exists()
    // {
    //     _senderMock.Setup(x => x.Send(It.IsAny<GetUserQuery>(), _cts.Token))
    //         .ReturnsAsync(_userDto)
    //         .Verifiable();
    //     
    //     var result = await _controller.GetUser(UserId, _cts.Token);
    //     
    //     Assert.Multiple(() =>
    //     {
    //         _senderMock.Verify();
    //         Assert.That(result, Is.Not.Null);
    //         Assert.That(result, Is.InstanceOf<OkObjectResult>());
    //         var okObjectResult = (OkObjectResult)result;
    //         Assert.That(okObjectResult.Value, Is.Not.Null);
    //         Assert.That(okObjectResult.Value, Is.InstanceOf<UserDto>());
    //         var userResult = (UserDto)okObjectResult.Value!;
    //         Assert.That(_userDto.Id, Is.EqualTo(userResult.Id));
    //     });
    // }
    //
    // [Test]
    // public void GetUser_When_UserNotFound_Should_ThrowUserNotFoundException()
    // {
    //     _senderMock.Setup(x => x.Send(It.IsAny<GetUserQuery>(), _cts.Token))
    //         .ThrowsAsync(new UserNotFoundException())
    //         .Verifiable();
    //     
    //     Assert.ThrowsAsync<UserNotFoundException>(async () => await _controller.GetUser(UserId, _cts.Token));
    // }
    
    [TearDown]
    public void AfterEachTest()
    {
        _cts.Dispose();
    }
}