using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Cristovao.Machado.Data.Migrations
{
    /// <inheritdoc />
    public partial class undoticketupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_ParkingSpace_ParkingSpaceId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Vehicle_VehicleId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpaceId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_ParkingSpace_ParkingSpaceId",
                table: "Ticket",
                column: "ParkingSpaceId",
                principalTable: "ParkingSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Vehicle_VehicleId",
                table: "Ticket",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_ParkingSpace_ParkingSpaceId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Vehicle_VehicleId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpaceId",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_ParkingSpace_ParkingSpaceId",
                table: "Ticket",
                column: "ParkingSpaceId",
                principalTable: "ParkingSpace",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Vehicle_VehicleId",
                table: "Ticket",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id");
        }
    }
}
