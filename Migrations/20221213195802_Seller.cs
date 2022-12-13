using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAuction.Migrations
{
    /// <inheritdoc />
    public partial class Seller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Sellers_SellerId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SellerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_User_UserId",
                table: "Sellers",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_User_UserId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sellers");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_SellerId",
                table: "User",
                column: "SellerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Sellers_SellerId",
                table: "User",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
