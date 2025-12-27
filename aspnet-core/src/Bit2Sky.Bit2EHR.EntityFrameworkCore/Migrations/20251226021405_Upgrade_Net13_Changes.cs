using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bit2Sky.Bit2EHR.Migrations
{
    /// <inheritdoc />
    public partial class Upgrade_Net13_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CreditBalance",
                table: "ChaiCountCustomers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FreeItemsRedeemed",
                table: "ChaiCountCustomers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsUdhariAccount",
                table: "ChaiCountCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "ChaiCountCustomers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChaiCountCreditTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ItemsSummary = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    LastSyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaiCountCreditTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChaiCountCreditTransactions_ChaiCountCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "ChaiCountCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChaiCountCreditTransactions_CustomerId",
                table: "ChaiCountCreditTransactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChaiCountCreditTransactions_TenantId_ClientId",
                table: "ChaiCountCreditTransactions",
                columns: new[] { "TenantId", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChaiCountCreditTransactions_TenantId_CustomerId",
                table: "ChaiCountCreditTransactions",
                columns: new[] { "TenantId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ChaiCountCreditTransactions_TenantId_TransactionDate",
                table: "ChaiCountCreditTransactions",
                columns: new[] { "TenantId", "TransactionDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChaiCountCreditTransactions");

            migrationBuilder.DropColumn(
                name: "CreditBalance",
                table: "ChaiCountCustomers");

            migrationBuilder.DropColumn(
                name: "FreeItemsRedeemed",
                table: "ChaiCountCustomers");

            migrationBuilder.DropColumn(
                name: "IsUdhariAccount",
                table: "ChaiCountCustomers");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "ChaiCountCustomers");
        }
    }
}
