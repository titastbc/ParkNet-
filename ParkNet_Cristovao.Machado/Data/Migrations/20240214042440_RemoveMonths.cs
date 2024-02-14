using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Cristovao.Machado.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMonths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodInMonths",
                table: "TariffPermits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodInMonths",
                table: "TariffPermits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
