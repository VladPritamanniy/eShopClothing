using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedClothingSizeTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Badlon" },
                    { 2, "Jumper" },
                    { 3, "Golf" },
                    { 4, "Sweater" },
                    { 5, "Pullover" },
                    { 6, "Blouse" },
                    { 7, "Shirt" },
                    { 8, "Trousers" },
                    { 9, "Breeches" },
                    { 10, "Capri" },
                    { 11, "Cargo" },
                    { 12, "Shorts" },
                    { 13, "Skirt" },
                    { 14, "Overalls" },
                    { 15, "Sweater" },
                    { 16, "Cardigan" },
                    { 17, "Jacket" },
                    { 18, "Blazer" },
                    { 19, "Top" },
                    { 20, "T-shirt" },
                    { 21, "Mike" },
                    { 22, "Polo" },
                    { 23, "Sweatshirt" },
                    { 24, "Raglan" },
                    { 25, "Dress" },
                    { 26, "Sundress" },
                    { 27, "Tunic" },
                    { 28, "Vest" },
                    { 29, "Windbreaker" },
                    { 30, "Jacket" },
                    { 31, "Coat" },
                    { 32, "Blouson" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "S" },
                    { 2, "M" },
                    { 3, "L" },
                    { 4, "XL" },
                    { 5, "XXL" },
                    { 6, "XXXL" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
