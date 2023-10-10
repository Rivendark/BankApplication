namespace RadancyBankApplication.Core.Exceptions;

public class DepositLimitExceededException : Exception
{
    private const string Message = "Deposit Limit ($10000) exceeded.";

    public DepositLimitExceededException() : base(Message) { }
}