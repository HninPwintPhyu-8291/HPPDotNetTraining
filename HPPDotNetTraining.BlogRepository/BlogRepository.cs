using HPPDotNetTraining.BlogRepository;
using System;
using System.Collections.Generic;
using System.Linq;

public class BlogRepository : IBlogRepository
{
    private readonly List<Blog> _blogs = new List<Blog>();
    private int _nextId = 1;

    public BlogRepository()
    {
        AddBlog(new Blog
        {
            BlogTitle = "Repository Pattern Explained",
            BlogAuthor = "Admin",
            BlogContent = "This is a detailed explanation of the repository pattern.",
            PublishedDate = DateTime.Now,
            Tags = new List<string> { "C#", ".NET", "Design Patterns" }
        });
        AddBlog(new Blog
        {
            BlogTitle = "Introduction to ASP.NET Core",
            BlogAuthor = "UserA",
            BlogContent = "A beginner's guide to building web apps with ASP.NET Core.",
            PublishedDate = DateTime.Now.AddDays(-5),
            Tags = new List<string> { "C#", ".NET", "Web Development" }
        });
    }

    public void AddBlog(Blog blog)
    {
        if (blog == null)
            throw new ArgumentNullException(nameof(blog));

        blog.BlogId = _nextId++;
        _blogs.Add(blog);
    }

    public void DeleteBlog(int blogId)
    {
        var blogToDelete = GetBlogById(blogId);
        if (blogToDelete != null)
        {
            _blogs.Remove(blogToDelete);
        }
    }

    public IEnumerable<Blog> GetAllBlogs()
    {
        return _blogs;
    }

    public IEnumerable<Blog> GetBlogsByAuthor(string author)
    {
        return _blogs.Where(b => b.BlogAuthor.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Blog GetBlogById(int blogId)
    {
        return _blogs.FirstOrDefault(b => b.BlogId == blogId);
    }

    public void UpdateBlog(Blog blog)
    {
        if (blog == null)
            throw new ArgumentNullException(nameof(blog));

        var existingBlog = GetBlogById(blog.BlogId);
        if (existingBlog != null)
        {
            existingBlog.BlogTitle = blog.BlogTitle;
            existingBlog.BlogAuthor = blog.BlogAuthor;
            existingBlog.BlogContent = blog.BlogContent;
            existingBlog.PublishedDate = blog.PublishedDate;
            existingBlog.Tags = blog.Tags;
        }
    }
}