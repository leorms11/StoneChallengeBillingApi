using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Domain.Models;

namespace StoneChallengeBillingApi.Application.Mappers;

public static class BillingDTOMapper
{
    public static Billing MapToEntity(this CreateBillingRequestDTO dto)
        => Billing.Create(dto.DueDate, dto.BillingAmount, dto.Cpf);

    public static IEnumerable<ListBillingsResponseDTO> MapToDto(this IEnumerable<Billing> billings)
    {
        foreach (var billing in billings)
            yield return new ListBillingsResponseDTO()
            {
                DueDate = billing.DueDate.ToString("dd/MM/yyyy"),
                CustomerCpf = billing.CustomerCpf.Value.ToString(@"000\.000\.000\-00"),
                BillingAmount = string.Format("{0:C}", billing.BillingAmount),
                CreatedAt = billing.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")
            };
    }
}