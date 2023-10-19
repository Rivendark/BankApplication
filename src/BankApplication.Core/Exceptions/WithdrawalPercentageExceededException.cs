namespace BankApplication.Core.Exceptions;

public class WithdrawalPercentageExceededException : Exception
{
    private new const string Message = "Withdrawal excceds 90% of account balance.";

    public WithdrawalPercentageExceededException() : base(Message) { }
}