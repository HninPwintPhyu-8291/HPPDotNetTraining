using Microsoft.Data.SqlClient;
using System;
using System.Configuration;

namespace ConsoleDeliveryCRUD
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
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Insert(); break;
                    case "2": Read(); break;
                    case "3": Update(); break;
                    case "4": Delete(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid option"); break;
                }
            }
        }

        static void Insert()
        {
            Console.Write("Delivery Name: "); string name = Console.ReadLine();
            Console.Write("Delivery Address: "); string address = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "INSERT INTO Tbl_Delivery (DeliveryName, DeliverAddress) VALUES (@Name, @Address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} record(s) inserted.");
            }
        }

        static void Read()
        {
            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "SELECT * FROM Tbl_Delivery";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\nId\tName\t\tAddress");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Id"]}\t{reader["DeliveryName"]}\t\t{reader["DeliverAddress"]}");
                }
            }
        }

        static void Update()
        {
            Console.Write("Enter Id to update: "); int id = int.Parse(Console.ReadLine());
            Console.Write("New Delivery Name: "); string name = Console.ReadLine();
            Console.Write("New Delivery Address: "); string address = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(stringBuilder.ConnectionString))
            {
                string query = "UPDATE Tbl_Delivery SET DeliveryName = @Name, DeliverAddress = @Address WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
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
                string query = "DELETE FROM Tbl_Delivery WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} record(s) deleted.");
            }
        }
    }
}
