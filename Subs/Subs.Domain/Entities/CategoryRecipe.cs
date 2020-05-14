using System;
using System.Collections.Generic;
using System.Text;

namespace Subs.Domain.Entities
{
    public class CategoryRecipe
    {
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipes { get; set; }
    }
}
