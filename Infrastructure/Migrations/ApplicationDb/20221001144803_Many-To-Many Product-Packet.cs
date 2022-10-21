using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ManyToManyProductPacket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Packets_PacketId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PacketId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PacketId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "PacketProduct",
                columns: table => new
                {
                    PacketsPacketId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketProduct", x => new { x.PacketsPacketId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_PacketProduct_Packets_PacketsPacketId",
                        column: x => x.PacketsPacketId,
                        principalTable: "Packets",
                        principalColumn: "PacketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacketProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacketProduct_ProductsProductId",
                table: "PacketProduct",
                column: "ProductsProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacketProduct");

            migrationBuilder.AddColumn<int>(
                name: "PacketId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PacketId",
                table: "Products",
                column: "PacketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Packets_PacketId",
                table: "Products",
                column: "PacketId",
                principalTable: "Packets",
                principalColumn: "PacketId");
        }
    }
}
