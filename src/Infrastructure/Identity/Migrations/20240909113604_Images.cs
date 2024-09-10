using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_AspNetUsers_ApplicationUserId",
                table: "Clothing");

            migrationBuilder.DropIndex(
                name: "IX_Clothing_ApplicationUserId",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Image",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Image");

            migrationBuilder.AddColumn<byte[]>(
                name: "Value",
                table: "Image",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Clothing_ApplicationUserId",
                table: "Clothing",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_AspNetUsers_ApplicationUserId",
                table: "Clothing",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
