using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingEvents.Migrations
{
    /// <inheritdoc />
    public partial class UserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Tags",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Events",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Categories",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatorId",
                table: "Categories",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatorId",
                table: "Categories",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_CreatorId",
                table: "Tags",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_CreatorId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CreatorId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Events_CreatorId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Categories");
        }
    }
}
