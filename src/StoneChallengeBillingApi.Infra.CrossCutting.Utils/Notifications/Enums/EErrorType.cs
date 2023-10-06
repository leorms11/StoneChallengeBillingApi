using System.ComponentModel;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Enums;

public enum EErrorType
{
    [Description("Dados inválidos!")]
    InvalidData = 1000,
    [Description("CPF inválido!")]
    InvalidCpf = 1002,
    [Description("Cliente não encontrado.")]
    CustomerNotFound = 1003,
    [Description("Parâmetros de rota inválidos!")]
    InvalidParameters = 1004,
}