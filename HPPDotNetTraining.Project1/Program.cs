using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace ConsoleCRUD
{
    class Program
    {
        static SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "2404NB600260\\SQLEXPRESS",
            InitialCatalog = "HPPDotNetTraining",
            UserID = "sa",
            Password = "hpp",
            TrustServerCertificate = true,
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1. Insert\n2. Read\n3. Update\n4. Delete\n5. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": Insert(); break;
                    case "2": Read(); break;
                    case "3": Update(); break;
                    case "4": Delete(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        static void Insert()
        {
            Console.Write("Name: "); var name = Console.ReadLine();
            Console.Write("Address: "); var address = Console.ReadLine();
            Console.Write("Email: "); var email = Console.ReadLine();
            Console.Write("Phone: "); var phone = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "INSERT INTO Tbl_Customer (Name, Address, Email, PhoneNo) VALUES (@Name, @Address, @Email, @PhoneNo)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PhoneNo", phone);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} record(s) inserted.");
            }
        }

        static void Read()
        {
            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "SELECT * FROM Tbl_Customer";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\nId\tName\tEmail\tPhone");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Id"]}\t{reader["Name"]}\t{reader["Email"]}\t{reader["PhoneNo"]}");
                }
            }
        }

        static void Update()
        {
            Console.Write("Enter Id to update: "); int id = int.Parse(Console.ReadLine());
            Console.Write("New Name: "); var name = Console.ReadLine();
            Console.Write("New Address: "); var address = Console.ReadLine();
            Console.Write("New Email: "); var email = Console.ReadLine();
            Console.Write("New Phone: "); var phone = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "UPDATE Tbl_Customer SET Name=@Name, Address=@Address, Email=@Email, PhoneNo=@PhoneNo WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PhoneNo", phone);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} record(s) updated.");
            }
        }

        static void Delete()
        {
            Console.Write("Enter Id to delete: "); int id = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "DELETE FROM Tbl_Customer WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} record(s) deleted.");
            }
        }
    }
}
