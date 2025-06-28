using System;
using System.Collections.Generic;

namespace HPPDotNetTraining.BlogRepositoryDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            IBlogRepository repository = new BlogRepository();
            BlogService service = new BlogService(repository);

            // CREATE
            service.CreateNewBlog("Dapper Blog", "Hnin", "This blog is created using Dapper.", new List<string> { "Dapper", "C#", ".NET" });

            // READ ALL
            var allBlogs = service.GetAllAvailableBlogs();
            Console.WriteLine("\n--- All Blogs ---");
            foreach (var blog in allBlogs)
            {
                Console.WriteLine($"ID        : {blog.BlogId}");
                Console.WriteLine($"Title     : {blog.BlogTitle}");
                Console.WriteLine($"Author    : {blog.BlogAuthor}");
                Console.WriteLine($"Published : {blog.PublishedDate}");
                Console.WriteLine($"Tags      : {string.Join(", ", blog.Tags)}");
                Console.WriteLine($"Content   : {blog.BlogContent}");
                Console.WriteLine(new string('-', 50));
            }

            // READ BY ID
            var blogDetails = service.GetBlogDetails(1);
            if (blogDetails != null)
            {
                Console.WriteLine($"\nBlog #1: {blogDetails.BlogTitle}");
            }

            // UPDATE
            blogDetails.BlogTitle = "Updated Blog Title";
            repository.UpdateBlog(blogDetails);
            Console.WriteLine("Blog updated.");

            // DELETE
            repository.DeleteBlog(blogDetails.BlogId);
            Console.WriteLine("Blog deleted.");
        }
    }
}
