namespace BankApplication.Core.Exceptions;

public class InsufficientAccountBalanceException : Exception
{
    private new const string Message = "Insufficient Account Balance.";

    public InsufficientAccountBalanceException() : base(Message){ }
}