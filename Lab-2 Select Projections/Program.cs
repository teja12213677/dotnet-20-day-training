using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}

// DTO class
public class ProductSummaryDto
{
    public string Name { get; set; }
    public string PriceLabel { get; set; }
}

class Program
{
    static void Main()
    {
        // Shared product dataset
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop",      Category = "Electronics", Price = 55000, InStock = true },
            new Product { Id = 2, Name = "Keyboard",    Category = "Electronics", Price = 999,   InStock = true },
            new Product { Id = 3, Name = "Mouse",       Category = "Electronics", Price = 750,   InStock = true },
            new Product { Id = 4, Name = "USB Cable",   Category = "Electronics", Price = 350,   InStock = true },

            new Product { Id = 5, Name = "Notebook",    Category = "Stationery",   Price = 250,   InStock = true },
            new Product { Id = 6, Name = "Pen Set",     Category = "Stationery",   Price = 150,   InStock = true },
            new Product { Id = 7, Name = "Backpack",    Category = "Stationery",   Price = 900,   InStock = false },

            new Product { Id = 8, Name = "Water Bottle", Category = "Accessories", Price = 600,   InStock = true },
            new Product { Id = 9, Name = "Wallet",       Category = "Accessories", Price = 850,   InStock = true },
            new Product { Id = 10, Name = "Sunglasses",  Category = "Accessories", Price = 1500,  InStock = false },

            new Product { Id = 11, Name = "Coffee Mug",  Category = "Home",        Price = 450,   InStock = true },
            new Product { Id = 12, Name = "Table Lamp",  Category = "Home",        Price = 1800,  InStock = true }
        };


        // =========================================================
        // 1. PROJECT TO JUST NAMES
        // =========================================================

        IEnumerable<string> names = products
            .Select(p => p.Name);

        Console.WriteLine("===== 1. PRODUCT NAMES =====");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }


        // =========================================================
        // 2. ANONYMOUS TYPE: NAME + PRICE WITH 18% TAX
        // =========================================================

        var productsWithTax = products
            .Select(p => new
            {
                Name = p.Name,
                PriceWithTax = p.Price * 1.18m
            });

        Console.WriteLine("\n===== 2. NAME + PRICE WITH 18% TAX =====");

        foreach (var product in productsWithTax)
        {
            Console.WriteLine(
                $"{product.Name} -> Rs.{product.PriceWithTax:F2}"
            );
        }


        // =========================================================
        // 3. PROJECT TO ProductSummaryDto
        // =========================================================

        IEnumerable<ProductSummaryDto> summaries = products
            .Select(p => new ProductSummaryDto
            {
                Name = p.Name,
                PriceLabel = $"Rs.{p.Price:F2}"
            });

        Console.WriteLine("\n===== 3. ProductSummaryDto =====");

        foreach (ProductSummaryDto product in summaries)
        {
            Console.WriteLine(
                $"{product.Name} -> {product.PriceLabel}"
            );
        }


        // =========================================================
        // 4. INDEX-AWARE SELECT
        // =========================================================

        IEnumerable<string> indexedProducts = products
            .Select((p, index) => $"#{index + 1}: {p.Name}");

        Console.WriteLine("\n===== 4. INDEX-AWARE SELECT =====");

        foreach (string product in indexedProducts)
        {
            Console.WriteLine(product);
        }
    }
}