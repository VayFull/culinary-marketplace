using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Subs.Domain.Entities;

namespace Subs.Infrastructure.Contexts
{
    public class SubsDbContext: IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<CategoryRecipe> CategoriesRecipes { get; set; }
        public DbSet<UserRecipe> UsersRecipes { get; set; }
        public SubsDbContext(DbContextOptions<SubsDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryRecipe>().HasKey(x => new { x.CategoryId, x.RecipeId });
            modelBuilder.Entity<UserRecipe>().HasKey(x => new { x.UserId, x.RecipeId });

            modelBuilder.Entity<CategoryRecipe>()
                .HasOne<Category>(e => e.Categories)
                .WithMany(p => p.CategoriesRecipes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryRecipe>()
                .HasOne<Recipe>(e => e.Recipes)
                .WithMany(p => p.CategoriesRecipes)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
