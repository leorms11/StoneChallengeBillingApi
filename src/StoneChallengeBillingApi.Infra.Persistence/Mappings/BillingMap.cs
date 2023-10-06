using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoneChallengeBillingApi.Domain.Models;

namespace StoneChallengeBillingApi.Infra.Persistence.Mappings;
public class BillingMap : BaseMap<Billing>
{
    public override void ConfigureCustom(EntityTypeBuilder<Billing> builder)
    {
        builder.ToTable("billings");

        builder.HasKey(x => x.Id)
            .HasName("pk_tb_billings");

        builder.Property(x => x.DueDate)
            .IsRequired()
            .HasColumnName("due_date")
            .HasColumnType("date");

        builder.Property(x => x.BillingAmount)
            .IsRequired()
            .HasColumnName("billing_amount")
            .HasColumnType("decimal");

        builder.OwnsOne(x => x.CustomerCpf,
            builderAction =>
            {
                builderAction.Property(x => x.Value)
                    .IsRequired()
                    .HasColumnName("customer_cpf")
                    .HasColumnType("bigint");

                builderAction.HasIndex(x => x.Value);
            });
    }
}
