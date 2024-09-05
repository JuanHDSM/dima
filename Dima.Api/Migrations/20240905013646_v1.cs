using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "VARCHAR(245)", maxLength: 245, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_role_claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleId = table.Column<long>(type: "bigint", nullable: false),
                    claimType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    claimValue = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_user",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalizedUserName = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalizedEmail = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    emailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    passwordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    securityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    twoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    lockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    accessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    paidOrReceivedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    type = table.Column<sbyte>(type: "TINYINT", nullable: false),
                    amount = table.Column<decimal>(type: "DECIMAL(65,30)", nullable: false),
                    categoryId = table.Column<long>(type: "bigint", nullable: false),
                    userId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_transaction_category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    claimType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    claimValue = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_claim_identity_user_userId",
                        column: x => x.userId,
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_login",
                columns: table => new
                {
                    loginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    providerKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    providerDisplayName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => new { x.loginProvider, x.providerKey });
                    table.ForeignKey(
                        name: "FK_identity_login_identity_user_userId",
                        column: x => x.userId,
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_role_identity_user_UserId",
                        column: x => x.UserId,
                        principalTable: "identity_user",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_user_role",
                columns: table => new
                {
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    roleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_user_role", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "FK_identity_user_role_identity_user_userId",
                        column: x => x.userId,
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "identity_user_token",
                columns: table => new
                {
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    loginProvider = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => new { x.userId, x.loginProvider, x.name });
                    table.ForeignKey(
                        name: "FK_identity_user_token_identity_user_userId",
                        column: x => x.userId,
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_identity_claim_userId",
                table: "identity_claim",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_login_userId",
                table: "identity_login",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_role_normalizedName",
                table: "identity_role",
                column: "normalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_identity_role_UserId",
                table: "identity_role",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_user_normalizedEmail",
                table: "identity_user",
                column: "normalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_identity_user_normalizedUserName",
                table: "identity_user",
                column: "normalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_categoryId",
                table: "transaction",
                column: "categoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "identity_claim");

            migrationBuilder.DropTable(
                name: "identity_login");

            migrationBuilder.DropTable(
                name: "identity_role");

            migrationBuilder.DropTable(
                name: "identity_role_claim");

            migrationBuilder.DropTable(
                name: "identity_user_role");

            migrationBuilder.DropTable(
                name: "identity_user_token");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "identity_user");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
