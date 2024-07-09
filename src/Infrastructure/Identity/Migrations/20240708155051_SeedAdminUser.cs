using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAdnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "ab3ea43c-2eb3-4943-987a-16d282fc27df", "5849bcbd-fa3c-46ad-8ac1-00ff8cd9a6d7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "31d68aca-74e6-4714-ae67-bcfa7eb347af", "79f392bf-21e3-4e59-a899-f2a066f3884a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31d68aca-74e6-4714-ae67-bcfa7eb347af");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3ea43c-2eb3-4943-987a-16d282fc27df");
        }
    }
}
