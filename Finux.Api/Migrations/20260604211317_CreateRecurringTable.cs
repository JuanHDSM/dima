using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finux.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateRecurringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "installments",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "installmentsType",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "recurringType",
                table: "transaction");

            migrationBuilder.AddColumn<long>(
                name: "RecurringId",
                table: "transaction",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "recurring",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    recurringType = table.Column<sbyte>(type: "TINYINT", nullable: true),
                    installmentsType = table.Column<sbyte>(type: "TINYINT", nullable: false),
                    installments = table.Column<sbyte>(type: "TINYINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recurring", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_RecurringId",
                table: "transaction",
                column: "RecurringId");

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_recurring_RecurringId",
                table: "transaction",
                column: "RecurringId",
                principalTable: "recurring",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transaction_recurring_RecurringId",
                table: "transaction");

            migrationBuilder.DropTable(
                name: "recurring");

            migrationBuilder.DropIndex(
                name: "IX_transaction_RecurringId",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "RecurringId",
                table: "transaction");

            migrationBuilder.AddColumn<sbyte>(
                name: "installments",
                table: "transaction",
                type: "TINYINT",
                nullable: true);

            migrationBuilder.AddColumn<sbyte>(
                name: "installmentsType",
                table: "transaction",
                type: "TINYINT",
                nullable: false,
                defaultValue: (sbyte)0);

            migrationBuilder.AddColumn<sbyte>(
                name: "recurringType",
                table: "transaction",
                type: "TINYINT",
                nullable: true);
        }
    }
}
