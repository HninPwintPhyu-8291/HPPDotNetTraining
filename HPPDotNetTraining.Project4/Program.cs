using HPPDotNetTraining.Project4;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Language CRUD Menu ===");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Read All");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": Insert(); break;
                case "2": ReadAll(); break;
                case "3": Update(); break;
                case "4": Delete(); break;
                case "5": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    static void Insert()
    {
        Console.Write("Language: ");
        var lang = Console.ReadLine();
        Console.Write("Title: ");
        var title = Console.ReadLine();
        Console.Write("Symbol: ");
        var symbol = Console.ReadLine();

        using var db = new AppDbContext();
        var language = new Language { LanguageName = lang, Title = title, Symbol = symbol };
        db.Languages.Add(language);
        db.SaveChanges();
        Console.WriteLine("Language added successfully.");
    }

    static void ReadAll()
    {
        using var db = new AppDbContext();
        var languages = db.Languages.ToList();
        foreach (var l in languages)
        {
            Console.WriteLine($"Id: {l.Id}, Language: {l.LanguageName}, Title: {l.Title}, Symbol: {l.Symbol}");
        }
    }

    static void Update()
    {
        Console.Write("Enter Id to update: ");
        int id = int.Parse(Console.ReadLine());

        using var db = new AppDbContext();
        var language = db.Languages.FirstOrDefault(l => l.Id == id);
        if (language == null)
        {
            Console.WriteLine("Language not found.");
            return;
        }

        Console.Write("New Language: ");
        language.LanguageName = Console.ReadLine();
        Console.Write("New Title: ");
        language.Title = Console.ReadLine();
        Console.Write("New Symbol: ");
        language.Symbol = Console.ReadLine();

        db.SaveChanges();
        Console.WriteLine("Language updated successfully.");
    }

    static void Delete()
    {
        Console.Write("Enter Id to delete: ");
        int id = int.Parse(Console.ReadLine());

        using var db = new AppDbContext();
        var language = db.Languages.FirstOrDefault(l => l.Id == id);
        if (language == null)
        {
            Console.WriteLine("Language not found.");
            return;
        }

        db.Languages.Remove(language);
        db.SaveChanges();
        Console.WriteLine("Language deleted successfully.");
    }
}

