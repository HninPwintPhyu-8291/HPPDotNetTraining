using System;
using System.Collections.Generic;
using System.Linq;

namespace HPPDotNetTraining.BlogRepositoryDapper
{
    public class BlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Blog GetBlogDetails(int id)
        {
            return _blogRepository.GetBlogById(id);
        }

        public void CreateNewBlog(string title, string author, string content, List<string> tags)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new Exception("Blog title and content cannot be empty.");

            var newBlog = new Blog
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
                PublishedDate = DateTime.UtcNow,
                Tags = tags
            };

            _blogRepository.AddBlog(newBlog);
        }

        public IEnumerable<Blog> GetAllAvailableBlogs()
        {
            return _blogRepository.GetAllBlogs().Where(b => b.PublishedDate <= DateTime.Now);
        }
    }
}
