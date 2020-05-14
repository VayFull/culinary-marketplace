using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Subs.Domain.Entities;
using Subs.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Subs.Services
{
    public class RecipeService
    {
        private readonly SubsDbContext _context;
        private IMemoryCache cache;
        public RecipeService(SubsDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
        }

        public async Task AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            int n = await _context.SaveChangesAsync(); //n-число новых записей в таблице
            if (n > 0)
            {
                cache.Set(recipe.Id, recipe, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            Recipe recipe = null;
            if (!cache.TryGetValue(id, out recipe))
            {
                recipe = await _context.Recipes.FirstOrDefaultAsync(p => p.Id == id);
                if (recipe != null)
                {
                    cache.Set(recipe.Id, recipe,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return recipe;
        }
    }
}
