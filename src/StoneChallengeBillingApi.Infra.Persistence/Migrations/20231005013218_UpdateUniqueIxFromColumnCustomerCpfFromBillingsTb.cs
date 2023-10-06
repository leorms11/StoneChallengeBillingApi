using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneChallengeBillingApi.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class UpdateUniqueIxFromColumnCustomerCpfFromBillingsTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_billings_customer_cpf",
                table: "billings");

            migrationBuilder.CreateIndex(
                name: "IX_billings_customer_cpf",
                table: "billings",
                column: "customer_cpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_billings_customer_cpf",
                table: "billings");

            migrationBuilder.CreateIndex(
                name: "IX_billings_customer_cpf",
                table: "billings",
                column: "customer_cpf",
                unique: true);
        }
    }
}
