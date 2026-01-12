using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finux.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shortName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    regularMarketChange = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    regularMarketChangePercent = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    regularMarketTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    regularMarketPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    regularMarketDayHigh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    regularMarketDayRange = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    regularMarketDayLow = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    regularMarketVolume = table.Column<long>(type: "bigint", nullable: false),
                    regularMarketPreviousClose = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    regularMarketOpen = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    fiftyTwoWeekRange = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fiftyTwoWeekLow = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    fiftyTwoWeekHigh = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    priceEarnings = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    earningsPerShare = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    logoUrl = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.symbol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stocks");
        }
    }
}
