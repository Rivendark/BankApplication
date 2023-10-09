namespace RadancyBankApplication.Core.Exceptions;

public class UserExistsException : Exception
{
    private const string Message = "User already exists.";

    public UserExistsException() : base(Message) { }
}