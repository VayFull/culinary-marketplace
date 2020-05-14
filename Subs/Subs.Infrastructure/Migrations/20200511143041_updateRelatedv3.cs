using Microsoft.EntityFrameworkCore.Migrations;

namespace Subs.Infrastructure.Migrations
{
    public partial class updateRelatedv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRecipes_Recipes_RecipeId",
                table: "UsersRecipes");

            migrationBuilder.DropIndex(
                name: "IX_UsersRecipes_RecipeId",
                table: "UsersRecipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersRecipes_RecipeId",
                table: "UsersRecipes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRecipes_Recipes_RecipeId",
                table: "UsersRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
