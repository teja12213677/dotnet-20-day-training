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
        // 1. CATEGORY ASCENDING + PRICE DESCENDING
        // =========================================================

        var categoryPriceSorted = products
            .OrderBy(p => p.Category)
            .ThenByDescending(p => p.Price);

        Console.WriteLine("=================================================");
        Console.WriteLine("1. CATEGORY ASCENDING + PRICE DESCENDING");
        Console.WriteLine("=================================================");

        PrintProducts(categoryPriceSorted);


        // =========================================================
        // 2. BUG VERSION
        // =========================================================

        var buggySort = products
            .OrderBy(p => p.Category)
            .OrderBy(p => p.Price);

        Console.WriteLine("\n=================================================");
        Console.WriteLine("2. BUGGY SORT");
        Console.WriteLine("OrderBy(Category).OrderBy(Price)");
        Console.WriteLine("=================================================");

        PrintProducts(buggySort);


        // =========================================================
        // 3. CORRECT VERSION
        // =========================================================

        var fixedSort = products
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Price);

        Console.WriteLine("\n=================================================");
        Console.WriteLine("3. FIXED SORT");
        Console.WriteLine("OrderBy(Category).ThenBy(Price)");
        Console.WriteLine("=================================================");

        PrintProducts(fixedSort);


        // =========================================================
        // 4. THREE-KEY SORT
        // =========================================================

        var threeKeySort = products
            .OrderByDescending(p => p.InStock)
            .ThenBy(p => p.Category)
            .ThenBy(p => p.Name);

        Console.WriteLine("\n=================================================");
        Console.WriteLine("4. THREE-KEY SORT");
        Console.WriteLine("InStock DESC → Category ASC → Name ASC");
        Console.WriteLine("=================================================");

        PrintProducts(threeKeySort);
    }


    // =========================================================
    // HELPER METHOD
    // =========================================================

    static void PrintProducts(IEnumerable<Product> products)
    {
        Console.WriteLine(
            $"{"Name",-15} {"Category",-15} {"Price",10} {"InStock",10}"
        );

        Console.WriteLine(
            new string('-', 55)
        );

        foreach (Product p in products)
        {
            Console.WriteLine(
                $"{p.Name,-15} " +
                $"{p.Category,-15} " +
                $"Rs.{p.Price,7:F2} " +
                $"{p.InStock,10}"
            );
        }
    }
}