namespace RadancyBankApplication.Core.Exceptions;

public class AccountNotFoundException : Exception
{
    private const string Message = "Account not found.";

    public AccountNotFoundException() : base(Message) { }
}