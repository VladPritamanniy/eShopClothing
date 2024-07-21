using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5849bcbd-fa3c-46ad-8ac1-00ff8cd9a6d7", null, "User", "USER" },
                    { "79f392bf-21e3-4e59-a899-f2a066f3884a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[,]
                {
                    { "31d68aca-74e6-4714-ae67-bcfa7eb347af", "clothingshop335@gmail.com", "CLOTHINGSHOP335@GMAIL.COM", "clothingshop335@gmail.com", "CLOTHINGSHOP335@GMAIL.COM", true, "AQAAAAIAAYagAAAAEPMZ0QofQpKGjWTMlAv06c6Iq52iYWW1xncrlj5ds6LMziw5bSnt2EIWeHDIS+drEg==", "S5MWCC4T5TGKHSNOTNKBL5J7OFA5SYSL", "3638e981-8e63-45ab-85d0-bb159299ae34", null, false, false, null, true, 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "UserId", "ClaimType", "ClaimValue" },
                values: new object[,]
                {
                    { 1, "31d68aca-74e6-4714-ae67-bcfa7eb347af", "Role", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "31d68aca-74e6-4714-ae67-bcfa7eb347af", "79f392bf-21e3-4e59-a899-f2a066f3884a" }
                });

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
