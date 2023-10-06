using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StoneChallengeBillingApi.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class CreateBillingTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "billings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    due_date = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    billing_amount = table.Column<decimal>(type: "decimal", nullable: false),
                    customer_cpf = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_billings", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_billings_customer_cpf",
                table: "billings",
                column: "customer_cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billings");
        }
    }
}
