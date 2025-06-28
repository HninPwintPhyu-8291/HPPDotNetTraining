using Microsoft.Data.SqlClient;
using Dapper;
using HPPDotNetTraining.BlogRepositoryDapper;
using System;
using System.Linq;

public class BlogRepository : IBlogRepository
{
    private readonly string _connectionString = "Server=2404NB600260\\SQLEXPRESS;Database=HPPDotNetTraining;User Id=sa;Password=hpp;TrustServerCertificate=True;";

    public IEnumerable<Blog> GetAllBlogs()
    {
        using var connection = new SqlConnection(_connectionString);
        var blogs = connection.Query<Blog>("SELECT * FROM Blogs").ToList();
        foreach (var blog in blogs)
        {
            blog.Tags = GetTagsForBlog(blog.BlogId).ToList();
        }
        return blogs;
    }

    public Blog GetBlogById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var blog = connection.QuerySingleOrDefault<Blog>("SELECT * FROM Blogs WHERE BlogId = @Id", new { Id = id });
        if (blog != null)
            blog.Tags = GetTagsForBlog(blog.BlogId).ToList();
        return blog;
    }

    public void AddBlog(Blog blog)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO Blogs (BlogTitle, BlogAuthor, BlogContent, PublishedDate) 
                    VALUES (@BlogTitle, @BlogAuthor, @BlogContent, @PublishedDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

        blog.PublishedDate = DateTime.Now;
        blog.BlogId = connection.QuerySingle<int>(sql, blog);

        foreach (var tag in blog.Tags)
        {
            AddTagToBlog(blog.BlogId, tag);
        }
    }

    public void UpdateBlog(Blog blog)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"UPDATE Blogs SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent 
                    WHERE BlogId = @BlogId";
        connection.Execute(sql, blog);

        // Clear old tags and insert new
        connection.Execute("DELETE FROM BlogTags WHERE BlogId = @Id", new { Id = blog.BlogId });
        foreach (var tag in blog.Tags)
        {
            AddTagToBlog(blog.BlogId, tag);
        }
    }

    public void DeleteBlog(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM BlogTags WHERE BlogId = @Id", new { Id = id });
        connection.Execute("DELETE FROM Blogs WHERE BlogId = @Id", new { Id = id });
    }

    private IEnumerable<string> GetTagsForBlog(int blogId)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<string>(@"
            SELECT t.TagName FROM Tags t
            INNER JOIN BlogTags bt ON bt.TagId = t.TagId
            WHERE bt.BlogId = @BlogId", new { BlogId = blogId });
    }

    private void AddTagToBlog(int blogId, string tagName)
    {
        using var connection = new SqlConnection(_connectionString);

        var tagId = connection.QuerySingleOrDefault<int?>("SELECT TagId FROM Tags WHERE TagName = @Name", new { Name = tagName });
        if (!tagId.HasValue)
        {
            tagId = connection.QuerySingle<int>("INSERT INTO Tags (TagName) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int);", new { Name = tagName });
        }

        connection.Execute("INSERT INTO BlogTags (BlogId, TagId) VALUES (@BlogId, @TagId)", new { BlogId = blogId, TagId = tagId });
    }

    public IEnumerable<Blog> GetBlogsByAuthor(string author)
    {
        using var connection = new SqlConnection(_connectionString);
        var blogs = connection.Query<Blog>("SELECT * FROM Blogs WHERE BlogAuthor = @Author", new { Author = author }).ToList();
        foreach (var blog in blogs)
        {
            blog.Tags = GetTagsForBlog(blog.BlogId).ToList();
        }
        return blogs;
    }
}