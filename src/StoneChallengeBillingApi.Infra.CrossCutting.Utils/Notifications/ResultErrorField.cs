using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;

public class ResultErrorField
{
    public ResultErrorField(string field, string message)
    {
        Field = field;
        Message = message;
    }
    
    [JsonPropertyName("field"), JsonProperty("field")]
    public string Field { get; }
    
    [JsonPropertyName("message"), JsonProperty("message")]
    public string Message { get; }
}