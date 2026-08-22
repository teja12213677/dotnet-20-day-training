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
        // Shared dataset: 12 products
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop",       Category = "Electronics", Price = 55000, InStock = true },
            new Product { Id = 2, Name = "Mouse",        Category = "Electronics", Price = 750,   InStock = true },
            new Product { Id = 3, Name = "Keyboard",     Category = "Electronics", Price = 1200,  InStock = true },
            new Product { Id = 4, Name = "USB Cable",    Category = "Electronics", Price = 350,   InStock = true },

            new Product { Id = 5, Name = "Notebook",     Category = "Stationery",   Price = 250,   InStock = true },
            new Product { Id = 6, Name = "Pen Set",      Category = "Stationery",   Price = 150,   InStock = true },
            new Product { Id = 7, Name = "Backpack",     Category = "Stationery",   Price = 900,   InStock = false },

            new Product { Id = 8, Name = "Water Bottle",  Category = "Accessories", Price = 600,   InStock = true },
            new Product { Id = 9, Name = "Wallet",        Category = "Accessories", Price = 850,   InStock = true },
            new Product { Id = 10, Name = "Sunglasses",   Category = "Accessories", Price = 1500,  InStock = false },

            new Product { Id = 11, Name = "Coffee Mug",   Category = "Home",        Price = 450,   InStock = true },
            new Product { Id = 12, Name = "Table Lamp",   Category = "Home",        Price = 1800,  InStock = true }
        };


        // =========================================================
        // (a) FULLY METHOD SYNTAX
        // =========================================================

        var resultA = products
            .Where(p => p.Price < 1000)
            .OrderBy(p => p.Name);


        // =========================================================
        // (b) FULLY QUERY SYNTAX
        // =========================================================

        var resultB =
            from p in products
            where p.Price < 1000
            orderby p.Name
            select p;


        // =========================================================
        // (c) QUERY SYNTAX WHERE + METHOD-SYNTAX ORDERBY
        // =========================================================

        var filteredC =
            from p in products
            where p.Price < 1000
            select p;

        var resultC = filteredC
            .OrderBy(p => p.Name);


        // =========================================================
        // (d) METHOD-SYNTAX WHERE + QUERY-SYNTAX ORDERBY
        // =========================================================

        var filteredD = products
            .Where(p => p.Price < 1000);

        var resultD =
            from p in filteredD
            orderby p.Name
            select p;


        // =========================================================
        // PRINT RESULTS
        // =========================================================

        Console.WriteLine("===== RESULT A: METHOD SYNTAX =====");
        PrintProducts(resultA);

        Console.WriteLine("\n===== RESULT B: QUERY SYNTAX =====");
        PrintProducts(resultB);

        Console.WriteLine("\n===== RESULT C: QUERY WHERE + METHOD ORDERBY =====");
        PrintProducts(resultC);

        Console.WriteLine("\n===== RESULT D: METHOD WHERE + QUERY ORDERBY =====");
        PrintProducts(resultD);


        // =========================================================
        // PROVE ALL FOUR RESULTS ARE IDENTICAL
        // =========================================================

        bool AB = resultA.SequenceEqual(resultB);
        bool AC = resultA.SequenceEqual(resultC);
        bool AD = resultA.SequenceEqual(resultD);

        Console.WriteLine("\n===== EQUIVALENCE CHECK =====");

        Console.WriteLine($"A == B : {AB}");
        Console.WriteLine($"A == C : {AC}");
        Console.WriteLine($"A == D : {AD}");

        if (AB && AC && AD)
        {
            Console.WriteLine("\nAll four queries produce identical results!");
        }
        else
        {
            Console.WriteLine("\nThe results are different.");
        }
    }


    // Helper method to print products
    static void PrintProducts(IEnumerable<Product> products)
    {
        foreach (Product p in products)
        {
            Console.WriteLine(
                $"Id: {p.Id}, Name: {p.Name}, " +
                $"Category: {p.Category}, Price: Rs.{p.Price}"
            );
        }
    }
}