// EfCoreOperations.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseCrudExample
{
    public class EfCoreOperations
    {
        public void PerformCrud()
        {
            Console.WriteLine("\nEF Core: Inserting new products (Create)");
            InsertProduct(new Product { Name = "EF Core Product P", Price = 100.00m });
            InsertProduct(new Product { Name = "EF Core Product Q", Price = 120.00m });

            Console.WriteLine("\nEF Core: Reading all products (Read All)");
            List<Product> products = GetProducts();
            DisplayHelper.DisplayProducts(products);

            Console.WriteLine("\nEF Core: Reading product by Id (Read By Id)");
            Product productById = GetProductById(products.FirstOrDefault()?.Id ?? 0);
            if (productById != null)
            {
                Console.WriteLine($"Id: {productById.Id}, Name: {productById.Name}, Price: {productById.Price}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.WriteLine("\nEF Core: Updating a product (Update)");
            if (productById != null)
            {
                productById.Name = "EF Core Updated Product P";
                productById.Price = 105.99m;
                UpdateProduct(productById);
                Console.WriteLine("Product updated.");
                DisplayHelper.DisplayProducts(GetProducts());
            }

            Console.WriteLine("\nEF Core: Deleting a product (Delete)");
            if (products.Count > 0)
            {
                DeleteProduct(products.LastOrDefault().Id);
                Console.WriteLine("Product deleted.");
                DisplayHelper.DisplayProducts(GetProducts());
            }
        }

        private void InsertProduct(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        private List<Product> GetProducts()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.ToList();
            }
        }

        private Product GetProductById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.Find(id);
            }
        }

        private void UpdateProduct(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Products.Update(product);
                context.SaveChanges();
            }
        }

        private void DeleteProduct(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var productToDelete = context.Products.Find(id);
                if (productToDelete != null)
                {
                    context.Products.Remove(productToDelete);
                    context.SaveChanges();
                }
            }
        }
    }
}