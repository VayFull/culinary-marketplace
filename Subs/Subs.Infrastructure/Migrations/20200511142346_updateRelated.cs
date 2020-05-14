using Microsoft.EntityFrameworkCore.Migrations;

namespace Subs.Infrastructure.Migrations
{
    public partial class updateRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRecipes_AspNetUsers_UserId1",
                table: "UsersRecipes");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRecipes_AspNetUsers_UserId1",
                table: "UsersRecipes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRecipes_AspNetUsers_UserId1",
                table: "UsersRecipes");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRecipes_AspNetUsers_UserId1",
                table: "UsersRecipes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
