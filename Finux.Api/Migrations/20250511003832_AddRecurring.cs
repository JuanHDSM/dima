using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRecurring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<sbyte>(
                name: "isRecurring",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRecurring",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "recurringType",
                table: "transaction");
        }
    }
}
