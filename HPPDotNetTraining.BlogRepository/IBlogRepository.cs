using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining.BlogRepository
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
