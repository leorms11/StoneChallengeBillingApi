using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Domain.Interfaces.Services;

public interface IBillingService
{
    Task<IOperation<Billing>> CreateAsync(Billing billing);
}