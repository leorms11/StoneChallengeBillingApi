namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

public interface IOperation
{
    string? Reason { get; }
    ResultError? Errors { get; }
}

public interface IOperation<T> : IOperation
{
    T Data { get; }
}