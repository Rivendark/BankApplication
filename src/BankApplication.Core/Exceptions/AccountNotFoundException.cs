namespace BankApplication.Core.Exceptions;

public class AccountNotFoundException : Exception
{
    private new const string Message = "Account not found.";

    public AccountNotFoundException() : base(Message) { }
}