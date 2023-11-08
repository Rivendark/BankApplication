namespace BankApplication.Core.Exceptions;

public sealed class InsufficientAccountBalanceException : Exception
{
    private new const string Message = "Insufficient Account Balance.";

    public InsufficientAccountBalanceException() : base(Message){ }
}