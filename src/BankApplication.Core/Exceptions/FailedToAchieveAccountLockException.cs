namespace BankApplication.Core.Exceptions;

public sealed class FailedToAchieveAccountLockException() : Exception(Message)
{
    private new const string Message = "Failed to achieve account lock.";
}