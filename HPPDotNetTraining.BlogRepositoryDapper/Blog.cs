using System;
using System.Collections.Generic;
using System.Linq;

namespace HPPDotNetTraining.BlogRepositoryDapper
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
        public DateTime PublishedDate { get; set; }

        public string TagsSerialized { get; set; }  // Stored in DB

        public List<string> Tags                    // Used in C#
        {
            get => string.IsNullOrEmpty(TagsSerialized)
                ? new List<string>()
                : TagsSerialized.Split(',').Select(t => t.Trim()).ToList();
            set => TagsSerialized = string.Join(",", value);
        }
    }
}
