namespace BankApplication.Core.Exceptions;

public class AccountExistsException : Exception
{
    private new const string Message = "Account already exists.";
    
    public AccountExistsException() : base(Message) { }
}