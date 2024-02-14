using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Cristovao.Machado.Data.Migrations
{
    /// <inheritdoc />
    public partial class NameforTicketPermit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TariffPermits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TariffPermits");
        }
    }
}
