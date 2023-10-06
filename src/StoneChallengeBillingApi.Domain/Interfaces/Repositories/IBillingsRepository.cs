using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Domain.Records;

namespace StoneChallengeBillingApi.Domain.Interfaces.Repositories;

public interface IBillingsRepository
{
    Task<Billing> CreateAsync(Billing billing);
    Task<IEnumerable<Billing>> ListAsync(ListBillingsQueryParametersRecord record);
}