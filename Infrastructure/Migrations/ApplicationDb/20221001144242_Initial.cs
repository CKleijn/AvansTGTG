using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanteenEmployees",
                columns: table => new
                {
                    CanteenEmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanteenEmployees", x => x.CanteenEmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    CanteenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferingHotMeals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.CanteenId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyCity = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountOfReports = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Packets",
                columns: table => new
                {
                    PacketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false),
                    PickUpDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatestPickUpTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEightteenPlusPacket = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    ReservedByStudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packets", x => x.PacketId);
                    table.ForeignKey(
                        name: "FK_Packets_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "CanteenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Packets_Students_ReservedByStudentId",
                        column: x => x.ReservedByStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAlcoholic = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PacketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Packets_PacketId",
                        column: x => x.PacketId,
                        principalTable: "Packets",
                        principalColumn: "PacketId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packets_CanteenId",
                table: "Packets",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_Packets_ReservedByStudentId",
                table: "Packets",
                column: "ReservedByStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PacketId",
                table: "Products",
                column: "PacketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanteenEmployees");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Packets");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
