using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_stocks",
                table: "stocks");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "stocks",
                type: "bigint",
                nullable: false)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_stocks",
                table: "stocks",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_symbol",
                table: "stocks",
                column: "symbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_stocks",
                table: "stocks");

            migrationBuilder.DropIndex(
                name: "IX_stocks_symbol",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "id",
                table: "stocks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stocks",
                table: "stocks",
                column: "symbol");
        }
    }
}
