namespace BankApplication.Core.Exceptions;

public sealed class AccountNotFoundException : Exception
{
    private new const string Message = "Account not found.";

    public AccountNotFoundException() : base(Message) { }
}