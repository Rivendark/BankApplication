namespace BankApplication.Core.Exceptions;

public class FailedToAchieveAccountLockException() : Exception(Message)
{
    private new const string Message = "Failed to achieve account lock.";
}