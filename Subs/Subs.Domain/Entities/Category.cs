using System;
using System.Collections.Generic;
using System.Text;

namespace Subs.Domain.Entities
{
    public class Category
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<CategoryRecipe> CategoriesRecipes { get; set; } = new List<CategoryRecipe>();
    }
}
