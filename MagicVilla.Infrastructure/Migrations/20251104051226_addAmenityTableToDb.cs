using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAmenityTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Description", "Name", "VillaId" },
                values: new object[,]
                {
                    { 1, "High-speed wireless internet access", "Free Wi-Fi", 1 },
                    { 2, "Outdoor pool with sun loungers", "Swimming Pool", 1 },
                    { 3, "Central air conditioning system", "Air Conditioning", 2 },
                    { 4, "Compact kitchen area with appliances", "Kitchenette", 2 },
                    { 5, "Secluded garden area with seating", "Private Garden", 3 },
                    { 6, "Outdoor barbecue grill for cooking", "BBQ Grill", 3 },
                    { 7, "Designated play area for children", "Kids Play Area", 4 },
                    { 8, "Video game console with games", "Game Console", 4 },
                    { 9, "Private jacuzzi with ocean view", "Jacuzzi", 5 },
                    { 10, "Special romantic decorations", "Romantic Decor", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_VillaId",
                table: "Amenities",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");
        }
    }
}
