using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;

namespace StoneChallengeBillingApi.Application.DTOs.Billings;

public class CreateBillingRequestDTO : Notifiable
{
    [JsonPropertyName("dueDate"), JsonProperty("dueDate")]
    public DateTime DueDate { get; init; }

    [JsonPropertyName("cpf"), JsonProperty("cpf")]
    public string Cpf { get; init; }

    [JsonPropertyName("billingAmount"), JsonProperty("billingAmount")]
    public decimal BillingAmount { get; init; }
}