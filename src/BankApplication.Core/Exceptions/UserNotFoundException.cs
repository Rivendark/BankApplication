namespace BankApplication.Core.Exceptions;

public class UserNotFoundException : Exception
{
    private new const string Message = "User not found.";

    public UserNotFoundException() : base(Message){ }
}