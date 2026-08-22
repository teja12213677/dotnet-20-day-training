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
        // 1. DEFERRED EXECUTION
        // =========================================================

        Console.WriteLine("=================================================");
        Console.WriteLine("1. DEFERRED EXECUTION");
        Console.WriteLine("=================================================");

        List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Keyboard",
                Category = "Electronics",
                Price = 999,
                InStock = true
            },

            new Product
            {
                Id = 2,
                Name = "Mouse",
                Category = "Electronics",
                Price = 750,
                InStock = true
            }
        };

        // Build the query.
        // The Where condition has NOT been executed yet.
        var deferredQuery = products
            .Where(p => p.Price < 1000);

        Console.WriteLine("Query built.");

        // Add a new product AFTER creating the query.
        products.Add(new Product
        {
            Id = 3,
            Name = "Keyboard Pad",
            Category = "Accessories",
            Price = 500,
            InStock = true
        });

        Console.WriteLine(
            "New product added to the original list."
        );

        Console.WriteLine("\nEnumerating deferred query:");

        foreach (Product product in deferredQuery)
        {
            Console.WriteLine(
                $"{product.Name} - Rs.{product.Price:F2}"
            );
        }

        Console.WriteLine(
            "\nThe newly added product appears because " +
            "the query was executed during enumeration."
        );


        // =========================================================
        // 2. IMMEDIATE EXECUTION WITH ToList()
        // =========================================================

        Console.WriteLine("\n=================================================");
        Console.WriteLine("2. IMMEDIATE EXECUTION WITH ToList()");
        Console.WriteLine("=================================================");

        List<Product> productsSnapshotSource = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Keyboard",
                Category = "Electronics",
                Price = 999,
                InStock = true
            },

            new Product
            {
                Id = 2,
                Name = "Mouse",
                Category = "Electronics",
                Price = 750,
                InStock = true
            }
        };

        // ToList() executes the query immediately.
        List<Product> snapshot = productsSnapshotSource
            .Where(p => p.Price < 1000)
            .ToList();

        Console.WriteLine(
            "Query built and immediately materialized with ToList()."
        );

        // Add a matching product AFTER ToList().
        productsSnapshotSource.Add(new Product
        {
            Id = 3,
            Name = "USB Cable",
            Category = "Electronics",
            Price = 350,
            InStock = true
        });

        Console.WriteLine(
            "New matching product added to the original list."
        );

        Console.WriteLine("\nEnumerating snapshot:");

        foreach (Product product in snapshot)
        {
            Console.WriteLine(
                $"{product.Name} - Rs.{product.Price:F2}"
            );
        }

        Console.WriteLine(
            "\nThe new USB Cable does NOT appear because " +
            "ToList() created a snapshot before it was added."
        );


        // =========================================================
        // 3. DOUBLE ENUMERATION OF A DEFERRED QUERY
        // =========================================================

        Console.WriteLine("\n=================================================");
        Console.WriteLine("3. DOUBLE ENUMERATION");
        Console.WriteLine("=================================================");

        List<Product> expensiveProducts = new List<Product>
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
                Price = 750,
                InStock = true
            }
        };

        // Deferred query with a simulated expensive operation.
        var expensiveQuery = expensiveProducts
            .Where(p =>
            {
                Console.WriteLine(
                    $"Checking price for {p.Name}..."
                );

                return p.Price > 500;
            });


        // ---------------------------------------------------------
        // FIRST ENUMERATION
        // ---------------------------------------------------------

        Console.WriteLine("\n--- First enumeration ---");

        foreach (Product product in expensiveQuery)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }


        // ---------------------------------------------------------
        // SECOND ENUMERATION
        // ---------------------------------------------------------

        Console.WriteLine("\n--- Second enumeration ---");

        foreach (Product product in expensiveQuery)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }

        Console.WriteLine(
            "\nNotice that 'Checking price...' appears again."
        );

        Console.WriteLine(
            "The predicate executed again for every product."
        );


        // =========================================================
        // FIX: MATERIALIZE ONCE
        // =========================================================

        Console.WriteLine("\n=================================================");
        Console.WriteLine("3B. FIX USING ToList()");
        Console.WriteLine("=================================================");

        var materializedProducts = expensiveProducts
            .Where(p =>
            {
                Console.WriteLine(
                    $"Checking price for {p.Name}..."
                );

                return p.Price > 500;
            })
            .ToList();

        Console.WriteLine(
            "\nQuery has now been executed once and materialized."
        );


        // ---------------------------------------------------------
        // FIRST ENUMERATION OF MATERIALIZED LIST
        // ---------------------------------------------------------

        Console.WriteLine("\n--- First enumeration of list ---");

        foreach (Product product in materializedProducts)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }


        // ---------------------------------------------------------
        // SECOND ENUMERATION OF MATERIALIZED LIST
        // ---------------------------------------------------------

        Console.WriteLine("\n--- Second enumeration of list ---");

        foreach (Product product in materializedProducts)
        {
            Console.WriteLine(
                $"Selected: {product.Name}"
            );
        }

        Console.WriteLine(
            "\nThe predicate does NOT run again."
        );

        Console.WriteLine(
            "The materialized list can be enumerated repeatedly " +
            "without re-running the LINQ query."
        );
    }
}