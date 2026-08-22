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

// =========================================================
// SUMMARY DTO
// =========================================================

public class CategorySummary
{
    public string Category { get; set; }
    public int ItemCount { get; set; }
    public decimal TotalValue { get; set; }
    public string TopProduct { get; set; }

    // Products inside the category, ordered by price descending
    public List<Product> Products { get; set; }
}


// =========================================================
// PROGRAM
// =========================================================

class Program
{
    static void Main()
    {
        // =====================================================
        // PRODUCT DATASET
        // =====================================================

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
                Price = 750,
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


        // =====================================================
        // QUERY SYNTAX VERSION
        // =====================================================

        var querySyntaxReport =
            from p in products
            where p.InStock
            group p by p.Category into categoryGroup
            let orderedProducts = categoryGroup
                .OrderByDescending(p => p.Price)
                .ToList()
            let totalValue = categoryGroup.Sum(p => p.Price)
            orderby totalValue descending
            select new CategorySummary
            {
                Category = categoryGroup.Key,
                ItemCount = categoryGroup.Count(),
                TotalValue = totalValue,
                TopProduct = orderedProducts.First().Name,
                Products = orderedProducts
            };


        // =====================================================
        // METHOD SYNTAX VERSION
        // =====================================================

        var methodSyntaxReport = products
            .Where(p => p.InStock)
            .GroupBy(p => p.Category)
            .Select(categoryGroup =>
            {
                List<Product> orderedProducts = categoryGroup
                    .OrderByDescending(p => p.Price)
                    .ToList();

                return new CategorySummary
                {
                    Category = categoryGroup.Key,
                    ItemCount = categoryGroup.Count(),
                    TotalValue = categoryGroup.Sum(p => p.Price),
                    TopProduct = orderedProducts.First().Name,
                    Products = orderedProducts
                };
            })
            .OrderByDescending(summary => summary.TotalValue)
            .ToList();


        // =====================================================
        // PRINT QUERY SYNTAX REPORT
        // =====================================================

        Console.WriteLine(
            "============================================================"
        );

        Console.WriteLine(
            "QUERY SYNTAX REPORT"
        );

        Console.WriteLine(
            "============================================================"
        );

        PrintReport(querySyntaxReport);


        // =====================================================
        // PRINT METHOD SYNTAX REPORT
        // =====================================================

        Console.WriteLine(
            "\n============================================================"
        );

        Console.WriteLine(
            "METHOD SYNTAX REPORT"
        );

        Console.WriteLine(
            "============================================================"
        );

        PrintReport(methodSyntaxReport);


        // =====================================================
        // COMPARE BOTH REPORTS
        // =====================================================

        bool reportsMatch =
            querySyntaxReport.Select(CreateComparisonKey)
            .SequenceEqual(
                methodSyntaxReport.Select(CreateComparisonKey)
            );

        Console.WriteLine(
            "\n============================================================"
        );

        Console.WriteLine(
            "EQUIVALENCE CHECK"
        );

        Console.WriteLine(
            "============================================================"
        );

        Console.WriteLine(
            $"Query syntax and method syntax match: {reportsMatch}"
        );
    }


    // =========================================================
    // PRINT REPORT
    // =========================================================

    static void PrintReport(IEnumerable<CategorySummary> report)
    {
        foreach (CategorySummary summary in report)
        {
            Console.WriteLine(
                $"\nCategory: {summary.Category}"
            );

            Console.WriteLine(
                $"Item Count: {summary.ItemCount}"
            );

            Console.WriteLine(
                $"Total Value: Rs.{summary.TotalValue:F2}"
            );

            Console.WriteLine(
                $"Top Product: {summary.TopProduct}"
            );

            Console.WriteLine(
                "Products:"
            );

            foreach (Product product in summary.Products)
            {
                Console.WriteLine(
                    $"  - {product.Name,-15} " +
                    $"Rs.{product.Price,10:F2}"
                );
            }
        }
    }


    // =========================================================
    // COMPARISON KEY
    // Used to compare both reports
    // =========================================================

    static string CreateComparisonKey(CategorySummary summary)
    {
        return
            $"{summary.Category}|" +
            $"{summary.ItemCount}|" +
            $"{summary.TotalValue:F2}|" +
            $"{summary.TopProduct}|" +
            string.Join(
                ",",
                summary.Products.Select(p => p.Name)
            );
    }
}