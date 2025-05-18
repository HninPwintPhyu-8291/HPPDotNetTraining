using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DapperConsoleCRUD
{
    class Program
    {
        // Set your SQL Server connection string here
        static SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "2404NB600260\\SQLEXPRESS", // Change if needed
            InitialCatalog = "HPPDotNetTraining",    // Your database name
            UserID = "sa",                           // SQL login
            Password = "hpp",                        // SQL password
            TrustServerCertificate = true
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== Teacher CRUD Menu =====");
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
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Teaching Subject: ");
            var subject = Console.ReadLine();
            Console.Write("Class: ");
            var className = Console.ReadLine();

            using (var conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string sql = "INSERT INTO Tbl_Teacher (Name, TeachingSubject, Class) VALUES (@Name, @TeachingSubject, @Class)";
                var result = conn.Execute(sql, new { Name = name, TeachingSubject = subject, Class = className });
                Console.WriteLine($"{result} record(s) inserted.");
            }
        }

        static void ReadAll()
        {
            using (var conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                var teachers = conn.Query<Teacher>("SELECT * FROM Tbl_Teacher");
                Console.WriteLine("\n--- Teachers List ---");
                foreach (var t in teachers)
                {
                    Console.WriteLine($"Id: {t.Id}, Name: {t.Name}, Teaching Subject: {t.TeachingSubject}, Class: {t.Class}");
                }
            }
        }

        static void Update()
        {
            Console.Write("Enter Id to update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("New Name: ");
            var name = Console.ReadLine();
            Console.Write("New Teaching Subject: ");
            var subject = Console.ReadLine();
            Console.Write("New Class: ");
            var className = Console.ReadLine();

            using (var conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string sql = "UPDATE Tbl_Teacher SET Name = @Name, TeachingSubject = @TeachingSubject, Class = @Class WHERE Id = @Id";
                var result = conn.Execute(sql, new { Id = id, Name = name, TeachingSubject = subject, Class = className });
                Console.WriteLine($"{result} record(s) updated.");
            }
        }

        static void Delete()
        {
            Console.Write("Enter Id to delete: ");
            int id = int.Parse(Console.ReadLine());

            using (var conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string sql = "DELETE FROM Tbl_Teacher WHERE Id = @Id";
                var result = conn.Execute(sql, new { Id = id });
                Console.WriteLine($"{result} record(s) deleted.");
            }
        }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeachingSubject { get; set; }
        public string Class { get; set; }
    }
}
