namespace RadancyBankApplication.Core.Exceptions;

public class UserNotFoundException : Exception
{
    private const string Message = "User not found.";

    public UserNotFoundException() : base(Message){ }
}