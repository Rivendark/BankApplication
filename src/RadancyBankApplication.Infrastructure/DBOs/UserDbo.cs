﻿using RadancyBankApplication.Core.Models;

namespace RadancyBankApplication.Infrastructure.DBOs;

public class UserDbo
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<AccountDbo> Accounts { get; set; } = new ();
    
    public UserDbo() {}

    public UserDbo(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Accounts = user.Accounts?.Select(x => new AccountDbo(x)).ToList() ?? new List<AccountDbo>();
    }

    public User ToDomainModel()
    {
        return new User
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Accounts = Accounts.Select(x => x.ToDomainModel()).ToList()
        };
    }
}