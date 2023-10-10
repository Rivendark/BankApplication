namespace RadancyBankApplication.Core.Exceptions;

public class AccountExistsException : Exception
{
    private const string Message = "Account already exists.";
    
    public AccountExistsException() : base(Message) { }
}