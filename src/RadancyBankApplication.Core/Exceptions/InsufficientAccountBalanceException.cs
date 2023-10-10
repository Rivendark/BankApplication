namespace RadancyBankApplication.Core.Exceptions;

public class InsufficientAccountBalanceException : Exception
{
    private const string Message = "Insufficient Account Balance.";

    public InsufficientAccountBalanceException() : base(Message){ }
}