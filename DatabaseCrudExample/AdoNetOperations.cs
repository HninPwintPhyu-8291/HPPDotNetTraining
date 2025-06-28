using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCrudExample
{
    public class AdoNetOperations
    {
        private readonly string _connectionString;

        public AdoNetOperations(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void PerformCrud()
        {
            Console.WriteLine("\nADO.NET: Inserting new products (Create)");
            InsertProduct(new Product { Name = "ADO.NET Product A", Price = 10.50m });
            InsertProduct(new Product { Name = "ADO.NET Product B", Price = 25.00m });

            Console.WriteLine("\nADO.NET: Reading all products (Read All)");
            List<Product> products = GetProducts();
            DisplayHelper.DisplayProducts(products);

            Console.WriteLine("\nADO.NET: Reading product by Id (Read By Id)");
            Product productById = GetProductById(products.FirstOrDefault()?.Id ?? 0);
            if (productById != null)
            {
                Console.WriteLine($"Id: {productById.Id}, Name: {productById.Name}, Price: {productById.Price}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.WriteLine("\nADO.NET: Updating a product (Update)");
            if (productById != null)
            {
                productById.Name = "ADO.NET Updated Product A";
                productById.Price = 12.75m;
                UpdateProduct(productById);
                Console.WriteLine("Product updated.");
                DisplayHelper.DisplayProducts(GetProducts());
            }

            Console.WriteLine("\nADO.NET: Deleting a product (Delete)");
            if (products.Count > 0)
            {
                DeleteProduct(products.LastOrDefault().Id);
                Console.WriteLine("Product deleted.");
                DisplayHelper.DisplayProducts(GetProducts());
            }
        }

        private void InsertProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.ExecuteNonQuery();
                }
            }
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Price FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }
            return products;
        }

        private Product GetProductById(int id)
        {
            Product product = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Price FROM Products WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            };
                        }
                    }
                }
            }
            return product;
        }

        private void UpdateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteProduct(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Products WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
    public static class DisplayHelper
    {
        public static void DisplayProducts(List<Product> products)
        {
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("Products List:");
            Console.WriteLine("---------------------------------");
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
            }
            Console.WriteLine("---------------------------------");
        }
    }
}
