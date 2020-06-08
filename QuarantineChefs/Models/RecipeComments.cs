using System;
using System.Collections.Generic;

namespace QuarantineChefs.Models
{
    public partial class RecipeComments
    {
        public int RecipeCommentsId { get; set; }
        public string Comments { get; set; }
        public int? UserProfileId { get; set; }
        public int? RecipeId { get; set; }

        public Recipe Recipe { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
