using Microsoft.EntityFrameworkCore;
using Subs.Domain.Entities;

namespace Subs.Infrastructure.Contexts
{
    public class SubsDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<CategoryRecipe> CategoryRecipes { get; set; }
        public SubsDbContext(DbContextOptions<SubsDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryRecipe>().HasKey(x => new { x.CategoryId, x.RecipeId });

            modelBuilder.Entity<CategoryRecipe>()
                .HasOne<Category>(e => e.Category)
                .WithMany(p => p.CategoryRecipes)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CategoryRecipe>()
                .HasOne<Recipe>(e => e.Recipe)
                .WithMany(p => p.CategoryRecipes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
