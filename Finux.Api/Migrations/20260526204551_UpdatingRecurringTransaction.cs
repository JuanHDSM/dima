using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finux.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingRecurringTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRecurring",
                table: "transaction",
                newName: "installmentsType");

            migrationBuilder.AddColumn<sbyte>(
                name: "installments",
                table: "transaction",
                type: "TINYINT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "installments",
                table: "transaction");

            migrationBuilder.RenameColumn(
                name: "installmentsType",
                table: "transaction",
                newName: "isRecurring");
        }
    }
}
