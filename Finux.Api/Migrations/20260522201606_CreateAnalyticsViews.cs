using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finux.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateAnalyticsViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW vwgetexpensesbycategory AS
                SELECT
                    tran.userId AS userId,
                    cat.title AS category,
                    YEAR(tran.paidOrReceivedAt) AS year,
                    MONTH(tran.paidOrReceivedAt) AS month,
                    SUM(tran.amount) AS expenses
                FROM
                    `transaction` tran
                JOIN
                    `category` cat
                    ON cat.id = tran.categoryId
                WHERE
                    tran.paidOrReceivedAt >= DATE_SUB(CURDATE(), INTERVAL 11 MONTH)
                    AND tran.paidOrReceivedAt <= DATE_ADD(CURDATE(), INTERVAL 1 MONTH)
                    AND tran.type = 2
                GROUP BY
                    tran.userId,
                    cat.title,
                    YEAR(tran.paidOrReceivedAt),
                    MONTH(tran.paidOrReceivedAt);
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW vwgetincomesandexpenses AS
                SELECT
                    tran.userId AS userId,
                    MONTH(tran.paidOrReceivedAt) AS month,
                    YEAR(tran.paidOrReceivedAt) AS year,
                    SUM(CASE WHEN tran.type = 1 THEN tran.amount ELSE 0 END) AS incomes,
                    SUM(CASE WHEN tran.type = 2 THEN tran.amount ELSE 0 END) AS expenses
                FROM
                    `transaction` tran
                WHERE
                    tran.paidOrReceivedAt >= CURDATE() - INTERVAL 11 MONTH
                    AND tran.paidOrReceivedAt <= CURDATE() + INTERVAL 1 MONTH
                GROUP BY
                    tran.userId,
                    MONTH(tran.paidOrReceivedAt),
                    YEAR(tran.paidOrReceivedAt);
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW vwgetincomesbycategory AS
                SELECT
                    tran.userId AS userId,
                    cat.title AS category,
                    YEAR(tran.paidOrReceivedAt) AS year,
                    MONTH(tran.paidOrReceivedAt) AS month,
                    SUM(tran.amount) AS incomes
                FROM
                    `transaction` tran
                JOIN
                    `category` cat
                    ON cat.id = tran.categoryId
                WHERE
                    tran.paidOrReceivedAt >= DATE_SUB(CURDATE(), INTERVAL 11 MONTH)
                    AND tran.paidOrReceivedAt <= DATE_ADD(CURDATE(), INTERVAL 1 MONTH)
                    AND tran.type = 1
                GROUP BY
                    tran.userId,
                    cat.title,
                    YEAR(tran.paidOrReceivedAt),
                    MONTH(tran.paidOrReceivedAt);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                     DROP VIEW IF EXISTS vwgetexpensesbycategory;
                                 """);

            migrationBuilder.Sql("""
                                     DROP VIEW IF EXISTS vwgetincomesandexpenses;
                                 """);

            migrationBuilder.Sql("""
                                     DROP VIEW IF EXISTS vwgetincomesbycategory;
                                 """);
        }
    }
}
