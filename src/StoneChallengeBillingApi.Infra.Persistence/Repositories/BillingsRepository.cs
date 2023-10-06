using Microsoft.EntityFrameworkCore;
using StoneChallengeBillingApi.Domain.Interfaces.Repositories;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Domain.Records;
using StoneChallengeBillingApi.Infra.Persistence.Context;

namespace StoneChallengeBillingApi.Infra.Persistence.Repositories;

public class BillingsRepository : IBillingsRepository
{
    private readonly PostgreSqlDbContext _dbContext;

    public BillingsRepository(PostgreSqlDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task<Billing> CreateAsync(Billing billing)
    {
        var entityEntry = await _dbContext.AddAsync(billing);
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public Task<IEnumerable<Billing>> ListAsync(ListBillingsQueryParametersRecord record)
    {
        var billingQueryable = _dbContext.Set<Billing>()
            .AsNoTracking()
            .AsQueryable();

        if (record.Cpf is not null)
            billingQueryable = billingQueryable.Where(x => x.CustomerCpf.Value == record.Cpf.Value);

        if (record.ReferenceDateEnd is not null && record.ReferenceDateInitial is not null)
            billingQueryable = billingQueryable.Where(x => x.DueDate >= record.ReferenceDateInitial &&
                                                        x.DueDate <= record.ReferenceDateEnd);

        return Task.FromResult(billingQueryable.AsEnumerable());
    }
}