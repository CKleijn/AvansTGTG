using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RemoveAmountOfReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packets_Canteens_CanteenId",
                table: "Packets");

            migrationBuilder.DropColumn(
                name: "AmountOfReports",
                table: "Students");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEightteenPlusPacket",
                table: "Packets",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "City",
                table: "Packets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CanteenId",
                table: "Packets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Packets_Canteens_CanteenId",
                table: "Packets",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "CanteenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packets_Canteens_CanteenId",
                table: "Packets");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfReports",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsEightteenPlusPacket",
                table: "Packets",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "City",
                table: "Packets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CanteenId",
                table: "Packets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Packets_Canteens_CanteenId",
                table: "Packets",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "CanteenId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
