using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using StoneChallengeBillingApi.Domain.Interfaces;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Helpers;
using StoneChallengeBillingApi.Infra.Persistence.Mappings;

namespace StoneChallengeBillingApi.Infra.Persistence.Context;

public class PostgreSqlDbContext : DbContext
{
    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
    {
        ChangeTracker.Tracked += OnEntityStateTracked;
    }

    public DbSet<Billing> Billings;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BillingMap());
    }
    
    private void OnEntityStateTracked(object sender, EntityTrackedEventArgs args)
    {
        if (args.Entry.Entity is ITimeStamped entity && args.Entry.State == EntityState.Added)
        {
            entity.CreatedAt = DateTimeHelpers.GetSouthAmericaDateTimeNow();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseLoggerFactory(LoggerFactory.Create(config =>
            {
                config.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                config.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
            }));
    }
}