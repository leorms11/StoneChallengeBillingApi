using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoneChallengeBillingApi.Domain.Models;

namespace StoneChallengeBillingApi.Infra.Persistence.Mappings
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureDefaultMap(builder);
            ConfigureCustom(builder);
        }

        private void ConfigureDefaultMap(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasColumnType("timestamp(6)");

            #region Ignores
            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.Notifications);
            #endregion
        }

        public abstract void ConfigureCustom(EntityTypeBuilder<T> builder);
    }
}
