using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Cristovao.Machado.Data.Migrations
{
    /// <inheritdoc />
    public partial class Upgrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "TariffPermits",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "period",
                table: "TariffPermits",
                newName: "Period");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "TariffPermits",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "TariffPermits",
                newName: "period");
        }
    }
}
