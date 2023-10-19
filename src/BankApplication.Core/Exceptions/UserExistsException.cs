namespace BankApplication.Core.Exceptions;

public class UserExistsException : Exception
{
    private new const string Message = "User already exists.";

    public UserExistsException() : base(Message) { }
}