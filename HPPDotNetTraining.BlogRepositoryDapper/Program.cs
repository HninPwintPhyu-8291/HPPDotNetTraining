using HPPDotNetTraining.BlogRepositoryDapper;
using Microsoft.Data.SqlClient;

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Repository ကို အရင်ဆုံး instance တစ်ခု တည်ဆောက်ပါ။
        // ဒါကတော့ ကျွန်တော်တို့ရဲ့ Data Layer ဖြစ်ပါတယ်။
        IBlogRepository blogRepository = new BlogRepository();

        // 2. Service Layer ကို တည်ဆောက်ပြီး အပေါ်က Repository ကို Dependency Injection အနေနဲ့ ထည့်ပေးလိုက်ပါ။
        BlogService blogService = new BlogService(blogRepository);

        Console.WriteLine("--- ရှိပြီးသော Blog များ ---");
        PrintBlogs(blogService.GetAllAvailableBlogs());

        // --- Blog အသစ်တစ်ခု ဖန်တီးခြင်း ---
        Console.WriteLine("--- Blog အသစ်တစ်ခု ဖန်တီးခြင်း ---");
        var newTags = new List<string> { "Console App", "Example" };
        blogService.CreateNewBlog("My New Blog", "Admin", "This is a new blog created from console.", newTags);
        Console.WriteLine("Blog အသစ် (ID: 3) ကို ဖန်တီးပြီးပါပြီ။");

        Console.WriteLine("--- လက်ရှိ Blog များအားလုံး ---");
        PrintBlogs(blogService.GetAllAvailableBlogs());

        // --- Blog တစ်ခုကို ID ဖြင့် ရှာဖွေခြင်း ---
        Console.WriteLine("--- Blog (ID: 1) ကို ရှာဖွေခြင်း ---");
        var foundBlog = blogService.GetBlogDetails(1);
        if (foundBlog != null)
        {
            Console.WriteLine($"တွေ့ရှိသော Blog: {foundBlog.BlogTitle} (By {foundBlog.BlogAuthor})");
        }

        // --- Blog တစ်ခုကို Update လုပ်ခြင်း ---
        Console.WriteLine("--- Blog (ID: 1) ကို Update လုပ်ခြင်း ---");
        var blogToUpdate = blogService.GetBlogDetails(1);
        if (blogToUpdate != null)
        {
            blogToUpdate.BlogTitle = "UPDATED: Repository Pattern Explained";
            blogRepository.UpdateBlog(blogToUpdate); // Service ကနေတစ်ဆင့်လည်းခေါ်နိုင်အောင် method ထပ်ထည့်နိုင်ပါတယ်။
            Console.WriteLine("Blog (ID: 1) ကို Update လုပ်ပြီးပါပြီ။");
            var updatedBlog = blogService.GetBlogDetails(1);
            Console.WriteLine($"Update ဖြစ်သွားသော Title: {updatedBlog.BlogTitle}");
        }

        // --- Blog တစ်ခုကို ဖျက်ခြင်း ---
        Console.WriteLine("--- Blog (ID: 2) ကို ဖျက်ခြင်း ---");
        blogRepository.DeleteBlog(2); // Service ကနေတစ်ဆင့်လည်းခေါ်နိုင်အောင် method ထပ်ထည့်နိုင်ပါတယ်။
        Console.WriteLine("Blog (ID: 2) ကို ဖျက်ပြီးပါပြီ။");

        Console.WriteLine("--- နောက်ဆုံး ကျန်ရှိသော Blog များ ---");
        PrintBlogs(blogService.GetAllAvailableBlogs());
    }

    // Blog စာရင်းကို Console မှာ ထုတ်ပေးမယ့် Helper Method
    public static void PrintBlogs(IEnumerable<Blog> blogs)
    {
        foreach (var blog in blogs)
        {
            Console.WriteLine($"ID: {blog.BlogId}, Title: {blog.BlogTitle}, Author: {blog.BlogAuthor}");
            Console.WriteLine($"    Content: {blog.BlogContent.Substring(0, Math.Min(30, blog.BlogContent.Length))}...");
            Console.WriteLine($"    Tags: [{string.Join(", ", blog.Tags)}]");
            Console.WriteLine();
        }
    }
}
