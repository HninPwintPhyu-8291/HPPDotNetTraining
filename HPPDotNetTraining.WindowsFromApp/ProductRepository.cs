using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining.WindowsFromApp
{
    public class ProductRepository
    {

        public void AddProduct(Product product)
        {
            string sql = @"INSERT INTO Products (Name, Quantity, Price, CreateDateTime, ModifiedDateTime)
                       VALUES (@Name, @Quantity, @Price, @CreateDateTime, @ModifiedDateTime)";
            using var connection = DbHelper.GetConnection();
            connection.Execute(sql, product);
        }

        public void UpdateProduct(Product product)
        {
            string sql = @"UPDATE Products SET Name = @Name, Quantity = @Quantity, Price = @Price,
                       ModifiedDateTime = @ModifiedDateTime WHERE Id = @Id";
            using var connection = DbHelper.GetConnection();
            connection.Execute(sql, product);
        }

        public void DeleteProduct(int id)
        {
            string sql = "DELETE FROM Products WHERE Id = @Id";
            using var connection = DbHelper.GetConnection();
            connection.Execute(sql, new { Id = id });
        }

        public Product GetProductById(int id)
        {
            string sql = "SELECT * FROM Products WHERE Id = @Id";
            using var connection = DbHelper.GetConnection();
            return connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string sql = "SELECT * FROM Products";
            using var connection = DbHelper.GetConnection();
            return connection.Query<Product>(sql);
        }
    }
}
