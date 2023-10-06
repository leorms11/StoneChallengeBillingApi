using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Application.UseCases.Billings.Interfaces;

public interface ICreateBillingUseCase
{
    Task<IOperation> ExecuteAsync(CreateBillingRequestDTO dto);
}