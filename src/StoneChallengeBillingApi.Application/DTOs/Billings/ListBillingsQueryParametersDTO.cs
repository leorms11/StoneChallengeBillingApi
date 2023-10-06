using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StoneChallengeBillingApi.Application.DTOs.Billings;

public class ListBillingsQueryParametersDTO
{
    [JsonPropertyName("referenceMonth"), JsonProperty("referenceMonth")]
    public string? ReferenceMonth { get; init; }
    
    [JsonPropertyName("cpf"), JsonProperty("cpf")]
    public string? Cpf { get; init; }
}