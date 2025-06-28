// DapperOperations.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseCrudExample
{
    public class DapperOperations
    {
        private readonly string _connectionString;

        public DapperOperations(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void PerformCrud()
        {
            Console.WriteLine("\nDapper: Inserting new products (Create)");
            InsertProduct(new Product { Name = "Dapper Product X", Price = 50.00m });
            InsertProduct(new Product { Name = "Dapper Product Y", Price = 75.25m });

            Console.WriteLine("\nDapper: Reading all products (Read All)");
            List<Product> products = GetProducts();
            DisplayHelper.DisplayProducts(products);

            Console.WriteLine("\nDapper: Reading product by Id (Read By Id)");
            Product productById = GetProductById(products.FirstOrDefault()?.Id ?? 0);
            if (productById != null)
            {
                Console.WriteLine($"Id: {productById.Id}, Name: {productById.Name}, Price: {productById.Price}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.WriteLine("\nDapper: Updating a product (Update)");
            if (productById != null)
            {
                productById.Name = "Dapper Updated Product X";
                productById.Price = 55.55m;
                UpdateProduct(productById);
                Console.WriteLine("Product updated.");
                DisplayHelper.DisplayProducts(GetProducts());
            }

            Console.WriteLine("\nDapper: Deleting a product (Delete)");
            if (products.Count > 0)
            {
                DeleteProduct(products.LastOrDefault().Id);
                Console.WriteLine("Product deleted.");
                DisplayHelper.DisplayProducts(GetProducts());
            }
        }

        private void InsertProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
                db.Execute(query, product);
            }
        }

        private List<Product> GetProducts()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Price FROM Products";
                return db.Query<Product>(query).ToList();
            }
        }

        private Product GetProductById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Price FROM Products WHERE Id = @Id";
                return db.QueryFirstOrDefault<Product>(query, new { Id = id });
            }
        }

        private void UpdateProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
                db.Execute(query, product);
            }
        }

        private void DeleteProduct(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Products WHERE Id = @Id";
                db.Execute(query, new { Id = id });
            }
        }
    }
}
