using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StoneChallengeBillingApi.Application.DTOs.Billings;

public class ListBillingsResponseDTO
{
    [JsonPropertyName("dueDate"), JsonProperty("dueDate")]
    public string DueDate { get; init; }
    
    [JsonPropertyName("customerCpf"), JsonProperty("customerCpf")]
    public string CustomerCpf { get; init; }
    
    [JsonPropertyName("billingAmount"), JsonProperty("billingAmount")]
    public string BillingAmount { get; init; }
    
    [JsonPropertyName("createdAt"), JsonProperty("createdAt")]
    public string CreatedAt { get; init; }
}