using System;
using System.Collections.Generic;

namespace QuarantineChefs.Models
{
    public partial class BlogComments
    {
        public int BlogCommentsId { get; set; }
        public string Comment { get; set; }
        public int? UserProfileId { get; set; }
        public int? BlogId { get; set; }

        public BlogArticle Blog { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
