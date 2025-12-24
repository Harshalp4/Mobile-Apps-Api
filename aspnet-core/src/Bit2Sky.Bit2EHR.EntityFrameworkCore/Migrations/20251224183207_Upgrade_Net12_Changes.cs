using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bit2Sky.Bit2EHR.Migrations
{
    /// <inheritdoc />
    public partial class Upgrade_Net12_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ChaiCountShopSettings",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChaiCountShopSettings");
        }
    }
}
