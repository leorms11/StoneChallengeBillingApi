using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;

public class FailedOperation : IOperation
{
    public FailedOperation() { }

    public FailedOperation(EErrorType type, IEnumerable<ResultErrorField> fields, string reason)
    {
        Reason = reason;
        Errors = new ResultError(type, fields);
    }
    
    public FailedOperation(ResultError error, string reason)
    {
        Reason = reason;
        Errors = error;
    }
    
    public FailedOperation(EErrorType type, string reason)
    {
        Reason = reason;
        Errors = new ResultError(type);
    }

    [JsonPropertyName("reason"), JsonProperty("reason")]
    public string? Reason { get; }
    
    [JsonPropertyName("errors"), JsonProperty("errors")]
    public ResultError? Errors { get; }
}

public class FailedOperation<T> : FailedOperation, IOperation<T>
{
    public T Data { get; private set; } = default!;
    
    public FailedOperation() { }

    public FailedOperation(EErrorType type, IEnumerable<ResultErrorField> fields, string reason)
    {
        Reason = reason;
        Errors = new ResultError(type, fields);
    }
    
    public FailedOperation(EErrorType type, string reason)
    {
        Reason = reason;
    }
    
    public FailedOperation(ResultError error, string reason)
    {
        Reason = reason;
        Errors = error;
    }

    [JsonPropertyName("reason"), JsonProperty("reason")]
    public string? Reason { get; }
    
    [JsonPropertyName("errors"), JsonProperty("errors")]
    public ResultError? Errors { get; }
}