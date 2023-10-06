using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;

public class SuccessOperation : IOperation
{
    public SuccessOperation() { }
    
    [JsonPropertyName("reason"), JsonProperty("reason")]
    public string? Reason { get; }
    
    [JsonPropertyName("erros"), JsonProperty("erros")]
    public ResultError? Errors { get; }
}