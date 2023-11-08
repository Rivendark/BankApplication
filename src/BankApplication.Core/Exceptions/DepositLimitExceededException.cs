namespace BankApplication.Core.Exceptions;

public sealed class DepositLimitExceededException : Exception
{
    private new const string Message = "Deposit Limit ($10000) exceeded.";

    public DepositLimitExceededException() : base(Message) { }
}