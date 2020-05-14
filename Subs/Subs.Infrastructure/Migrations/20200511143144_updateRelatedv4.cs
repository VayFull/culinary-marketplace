using Microsoft.EntityFrameworkCore.Migrations;

namespace Subs.Infrastructure.Migrations
{
    public partial class updateRelatedv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRecipes_AspNetUsers_UserId1",
                table: "UsersRecipes");

            migrationBuilder.DropIndex(
                name: "IX_UsersRecipes_UserId1",
                table: "UsersRecipes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UsersRecipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UsersRecipes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersRecipes_UserId1",
                table: "UsersRecipes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRecipes_AspNetUsers_UserId1",
                table: "UsersRecipes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
