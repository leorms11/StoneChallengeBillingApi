using StoneChallengeBillingApi.Domain.Interfaces.Repositories;
using StoneChallengeBillingApi.Domain.Interfaces.Services;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Domain.Services;

public class BillingService : IBillingService
{
    private readonly IBillingsRepository _billingsRepository;
    
    public BillingService(IBillingsRepository billingsRepository)
    {
        _billingsRepository = billingsRepository;
    }
    
    public async Task<IOperation<Billing>> CreateAsync(Billing billing)
    {
        if (!billing.IsValid)
            return Result.CreateFailure<Billing>(EErrorType.InvalidData, billing.Notifications);

        var createdBilling = await _billingsRepository.CreateAsync(billing);

        return Result.CreateSuccess(billing);
    }
}