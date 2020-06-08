using System;
using System.Collections.Generic;

namespace QuarantineChefs.Models
{
    public partial class BlogArticle
    {
        public BlogArticle()
        {
            BlogComments = new HashSet<BlogComments>();
        }

        public int BlogArticleId { get; set; }
        public string Title { get; set; }
        public string BlogText { get; set; }

        public ICollection<BlogComments> BlogComments { get; set; }
    }
}
