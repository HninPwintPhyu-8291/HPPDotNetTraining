using System.Collections.Generic;

namespace HPPDotNetTraining.BlogRepositoryDapper
{
    public interface IBlogRepository
    {
        Blog GetBlogById(int blogId);
        IEnumerable<Blog> GetAllBlogs();
        IEnumerable<Blog> GetBlogsByAuthor(string author);
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(int blogId);
    }
}
