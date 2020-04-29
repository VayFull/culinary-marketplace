using System;
using System.Collections.Generic;
using System.Text;

namespace Subs.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFree { get; set; }
        public int? Price { get; set; }
        public List<CategoryRecipe> CategoryRecipes { get; set; } = new List<CategoryRecipe>();
    }
}
