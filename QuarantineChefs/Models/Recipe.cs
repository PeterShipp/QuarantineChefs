using System;
using System.Collections.Generic;

namespace QuarantineChefs.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeComments = new HashSet<RecipeComments>();
        }

        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string RecipeText { get; set; }

        public ICollection<RecipeComments> RecipeComments { get; set; }
    }
}
