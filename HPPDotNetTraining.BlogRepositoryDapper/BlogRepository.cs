using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HPPDotNetTraining.BlogRepositoryDapper
{
    public class BlogRepository : IBlogRepository
    {
        public void AddBlog(Blog blog)
        {
            using var connection = DbHelper.GetConnection();
            string sql = @"INSERT INTO Blogs (BlogTitle, BlogAuthor, BlogContent, PublishedDate, Tags)
                           VALUES (@BlogTitle, @BlogAuthor, @BlogContent, @PublishedDate, @TagsSerialized)";
            connection.Execute(sql, blog);
        }

        public void DeleteBlog(int blogId)
        {
            using var connection = DbHelper.GetConnection();
            string sql = "DELETE FROM Blogs WHERE BlogId = @BlogId";
            connection.Execute(sql, new { BlogId = blogId });
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            using var connection = DbHelper.GetConnection();
            string sql = "SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, PublishedDate, Tags AS TagsSerialized FROM Blogs";
            return connection.Query<Blog>(sql).ToList();
        }

        public Blog GetBlogById(int blogId)
        {
            using var connection = DbHelper.GetConnection();
            string sql = "SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, PublishedDate, Tags AS TagsSerialized FROM Blogs WHERE BlogId = @BlogId";
            return connection.QuerySingleOrDefault<Blog>(sql, new { BlogId = blogId });
        }

        public IEnumerable<Blog> GetBlogsByAuthor(string author)
        {
            using var connection = DbHelper.GetConnection();
            string sql = "SELECT BlogId, BlogTitle, BlogAuthor, BlogContent, PublishedDate, Tags AS TagsSerialized FROM Blogs WHERE BlogAuthor = @Author";
            return connection.Query<Blog>(sql, new { Author = author }).ToList();
        }

        public void UpdateBlog(Blog blog)
        {
            using var connection = DbHelper.GetConnection();
            string sql = @"UPDATE Blogs SET 
                            BlogTitle = @BlogTitle,
                            BlogAuthor = @BlogAuthor,
                            BlogContent = @BlogContent,
                            PublishedDate = @PublishedDate,
                            Tags = @TagsSerialized
                           WHERE BlogId = @BlogId";
            connection.Execute(sql, blog);
        }
    }
}
