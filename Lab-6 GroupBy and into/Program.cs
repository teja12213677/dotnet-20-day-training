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

class Program
{
    static void Main()
    {
        // =========================================================
        // SHARED DATASET
        // =========================================================

        List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Category = "Electronics",
                Price = 55000,
                InStock = true
            },

            new Product
            {
                Id = 2,
                Name = "Keyboard",
                Category = "Electronics",
                Price = 999,
                InStock = true
            },

            new Product
            {
                Id = 3,
                Name = "Mouse",
                Category = "Electronics",
                Price = 450,
                InStock = true
            },

            new Product
            {
                Id = 4,
                Name = "USB Cable",
                Category = "Electronics",
                Price = 350,
                InStock = false
            },

            new Product
            {
                Id = 5,
                Name = "Notebook",
                Category = "Stationery",
                Price = 250,
                InStock = true
            },

            new Product
            {
                Id = 6,
                Name = "Pen Set",
                Category = "Stationery",
                Price = 150,
                InStock = true
            },

            new Product
            {
                Id = 7,
                Name = "Backpack",
                Category = "Stationery",
                Price = 900,
                InStock = false
            },

            new Product
            {
                Id = 8,
                Name = "Water Bottle",
                Category = "Accessories",
                Price = 600,
                InStock = true
            },

            new Product
            {
                Id = 9,
                Name = "Wallet",
                Category = "Accessories",
                Price = 850,
                InStock = true
            },

            new Product
            {
                Id = 10,
                Name = "Sunglasses",
                Category = "Accessories",
                Price = 1500,
                InStock = false
            },

            new Product
            {
                Id = 11,
                Name = "Coffee Mug",
                Category = "Home",
                Price = 450,
                InStock = true
            },

            new Product
            {
                Id = 12,
                Name = "Table Lamp",
                Category = "Home",
                Price = 1800,
                InStock = true
            }
        };


        // =========================================================
        // 1. GROUP BY CATEGORY
        // Print category and count
        // =========================================================

        var categoryGroups = products
            .GroupBy(p => p.Category);

        Console.WriteLine("=================================================");
        Console.WriteLine("1. PRODUCTS GROUPED BY CATEGORY");
        Console.WriteLine("=================================================");

        foreach (var group in categoryGroups)
        {
            Console.WriteLine(
                $"Category: {group.Key}, Count: {group.Count()}"
            );
        }


        // =========================================================
        // 2. QUERY SYNTAX + INTO
        //
        // Group by Category
        // Keep groups with 3 or more products
        // Order by total inventory value descending
        // =========================================================

        var largeCategories =
            from p in products
            group p by p.Category into categoryGroup
            where categoryGroup.Count() >= 3
            orderby categoryGroup.Sum(p => p.Price) descending
            select categoryGroup;

        Console.WriteLine("\n=================================================");
        Console.WriteLine("2. CATEGORIES WITH 3+ PRODUCTS");
        Console.WriteLine("ORDERED BY TOTAL INVENTORY VALUE DESCENDING");
        Console.WriteLine("=================================================");

        foreach (var group in largeCategories)
        {
            decimal totalValue = group.Sum(p => p.Price);

            Console.WriteLine(
                $"Category: {group.Key}, " +
                $"Count: {group.Count()}, " +
                $"Total Value: Rs.{totalValue:F2}"
            );
        }


        // =========================================================
        // 3. CATEGORY REPORT
        //
        // Count
        // Total value
        // Average price
        // Most expensive product
        // =========================================================

        Console.WriteLine("\n=================================================");
        Console.WriteLine("3. DETAILED CATEGORY REPORT");
        Console.WriteLine("=================================================");

        foreach (var group in products.GroupBy(p => p.Category))
        {
            int count = group.Count();

            decimal totalValue = group.Sum(p => p.Price);

            decimal averagePrice = group.Average(p => p.Price);

            Product mostExpensive = group
                .OrderByDescending(p => p.Price)
                .First();

            Console.WriteLine($"\nCategory: {group.Key}");
            Console.WriteLine($"Count: {count}");
            Console.WriteLine($"Total Value: Rs.{totalValue:F2}");
            Console.WriteLine($"Average Price: Rs.{averagePrice:F2}");
            Console.WriteLine(
                $"Most Expensive Product: {mostExpensive.Name}"
            );
        }


        // =========================================================
        // 4. COMPOSITE KEY
        // Group by Category + InStock
        // =========================================================

        var compositeGroups = products
            .GroupBy(p => new
            {
                p.Category,
                p.InStock
            });

        Console.WriteLine("\n=================================================");
        Console.WriteLine("4. GROUP BY CATEGORY + INSTOCK");
        Console.WriteLine("=================================================");

        foreach (var group in compositeGroups)
        {
            Console.WriteLine(
                $"Category: {group.Key.Category}, " +
                $"InStock: {group.Key.InStock}, " +
                $"Count: {group.Count()}"
            );
        }
    }
}