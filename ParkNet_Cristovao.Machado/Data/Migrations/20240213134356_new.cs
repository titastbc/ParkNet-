using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkNet_Cristovao.Machado.Data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permit_Veihicle_VehicleId",
                table: "Permit");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Veihicle_VehicleId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Veihicle_AspNetUsers_UserId",
                table: "Veihicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veihicle",
                table: "Veihicle");

            migrationBuilder.RenameTable(
                name: "Veihicle",
                newName: "Vehicle");

            migrationBuilder.RenameIndex(
                name: "IX_Veihicle_UserId",
                table: "Vehicle",
                newName: "IX_Vehicle_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permit_Vehicle_VehicleId",
                table: "Permit",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Vehicle_VehicleId",
                table: "Ticket",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AspNetUsers_UserId",
                table: "Vehicle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permit_Vehicle_VehicleId",
                table: "Permit");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Vehicle_VehicleId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AspNetUsers_UserId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Veihicle");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_UserId",
                table: "Veihicle",
                newName: "IX_Veihicle_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veihicle",
                table: "Veihicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permit_Veihicle_VehicleId",
                table: "Permit",
                column: "VehicleId",
                principalTable: "Veihicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Veihicle_VehicleId",
                table: "Ticket",
                column: "VehicleId",
                principalTable: "Veihicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Veihicle_AspNetUsers_UserId",
                table: "Veihicle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
