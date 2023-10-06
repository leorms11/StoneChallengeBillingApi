﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StoneChallengeBillingApi.Infra.Persistence.Context;

#nullable disable

namespace StoneChallengeBillingApi.Infra.Persistence.Migrations
{
    [DbContext(typeof(PostgreSqlDbContext))]
    [Migration("20231005032136_ChangeTypeOfColumnDueDateToDateInBillingsTb")]
    partial class ChangeTypeOfColumnDueDateToDateInBillingsTb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StoneChallenge.BillingApi.Domain.Models.Billing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BillingAmount")
                        .HasColumnType("decimal")
                        .HasColumnName("billing_amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("date")
                        .HasColumnName("due_date");

                    b.HasKey("Id")
                        .HasName("pk_tb_billings");

                    b.ToTable("billings", (string)null);
                });

            modelBuilder.Entity("StoneChallenge.BillingApi.Domain.Models.Billing", b =>
                {
                    b.OwnsOne("StoneChallenge.BillingApi.Domain.ValueObjects.Cpf", "CustomerCpf", b1 =>
                        {
                            b1.Property<int>("BillingId")
                                .HasColumnType("integer");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("customer_cpf");

                            b1.HasKey("BillingId");

                            b1.HasIndex("Value");

                            b1.ToTable("billings");

                            b1.WithOwner()
                                .HasForeignKey("BillingId");
                        });

                    b.Navigation("CustomerCpf")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
