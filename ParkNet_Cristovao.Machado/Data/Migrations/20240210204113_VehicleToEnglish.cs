using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Cristovao.Machado.Data.Migrations
{
    /// <inheritdoc />
    public partial class VehicleToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Veihicle",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Matricula",
                table: "Veihicle",
                newName: "Plate");

            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Veihicle",
                newName: "Factory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Veihicle",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "Plate",
                table: "Veihicle",
                newName: "Matricula");

            migrationBuilder.RenameColumn(
                name: "Factory",
                table: "Veihicle",
                newName: "Marca");
        }
    }
}
