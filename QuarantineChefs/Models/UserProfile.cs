using System;
using System.Collections.Generic;

namespace QuarantineChefs.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            BlogComments = new HashSet<BlogComments>();
            RecipeComments = new HashSet<RecipeComments>();
        }

        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserAccountId { get; set; }

        public ICollection<BlogComments> BlogComments { get; set; }
        public ICollection<RecipeComments> RecipeComments { get; set; }
    }
}
