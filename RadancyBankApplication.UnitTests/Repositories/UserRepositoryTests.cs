using Microsoft.EntityFrameworkCore;
using RadancyBankApplication.Core.Exceptions;
using RadancyBankApplication.Core.Models;
using RadancyBankApplication.Infrastructure.Contexts;
using RadancyBankApplication.Infrastructure.DBOs;
using RadancyBankApplication.Infrastructure.Repositories;

namespace RadancyBankApplication.UnitTests.Repositories;

[TestFixture]
public class UserRepositoryTests
{
    private BankDbContext _context;
    private UserRepository _userRepository;
    private Guid _userId = Guid.NewGuid();
    private UserDbo _firstUser;
    private CancellationTokenSource _cts;

    [SetUp]
    public async Task BeforeEachTest()
    {
        _cts = new CancellationTokenSource();
        
        var builder = new DbContextOptionsBuilder<BankDbContext>();
        builder.UseInMemoryDatabase(databaseName: "BankDb");

        var dbContextOptions = builder.Options;
        _context = new BankDbContext(dbContextOptions);
        // Delete existing db before creating a new one
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _firstUser = new UserDbo
        {
            Id = _userId,
            Email = "firstUser@test.com",
            FirstName = "First",
            LastName = "User"
        };
        
        _context.Users.Add(_firstUser);
        await _context.SaveChangesAsync();

        _userRepository = new UserRepository(_context);
    }

    [Test]
    public async Task Should_Create_New_User_If_Not_Exists()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "User"
        };

        await _userRepository.CreateUserAsync(user, _cts.Token);

        var result = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken: _cts.Token);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(userId), "User Id does not match.");
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName), "First Name does not match");
            Assert.That(result.LastName, Is.EqualTo(user.LastName), "Last Name does not match.");
            Assert.That(result.Email, Is.EqualTo(user.Email), "Email does not match.");
        });
    }

    [Test]
    public void Should_Throw_UserExistsException_On_Create_User_If_Exists()
    {
        var user = new User
        {
            Id = _userId,
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "User"
        };

        Assert.ThrowsAsync<UserExistsException>(() => _userRepository.CreateUserAsync(user, _cts.Token));
    }

    [Test]
    public async Task Should_Find_User_If_Exists()
    {
        var result = await _userRepository.GetUserAsync(_userId, _cts.Token);
        
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.IsInstanceOf<User>(result);
            Assert.That(_userId, Is.EqualTo(result.Id), "User Id does not match.");
        });
    }

    [Test]
    public async Task Should_Return_Null_On_User_Not_Found()
    {
        var result = await _userRepository.GetUserAsync(Guid.NewGuid(), _cts.Token);
        
        Assert.Null(result);
    }

    [Test]
    public async Task Should_Update_User_If_Found()
    {
        var firstNameChange = "Last";
        _firstUser.FirstName = firstNameChange;

        await _userRepository.UpdateUserAsync(_firstUser.ToDomainModel(), _cts.Token);

        var result = await _context.Users.FindAsync(_userId);
        
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.IsInstanceOf<UserDbo>(result);
            Assert.That(firstNameChange, Is.EqualTo(result.FirstName));
        });
    }

    [Test]
    public void Should_Throw_UserNotFoundException_On_Update_If_User_Not_Found()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "User"
        };

        Assert.ThrowsAsync<UserNotFoundException>(() => _userRepository.UpdateUserAsync(user, _cts.Token));
    }

    [Test]
    public async Task Should_Delete_User_If_Found()
    {
        await _userRepository.DeleteUserAsync(_userId, _cts.Token);
        
        var result = await _context.Users.FindAsync(_userId);
        
        Assert.Null(result);
    }

    [Test]
    public void Should_Throw_UserNotFoundException_On_Delete_If_User_Not_Found()
    {
        Assert.ThrowsAsync<UserNotFoundException>(() => _userRepository.DeleteUserAsync(Guid.NewGuid(), _cts.Token));
    }
}