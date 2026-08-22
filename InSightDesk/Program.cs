using System;
using System.Collections.Generic;
using System.Linq;

namespace InsightDesk
{
    // =========================================================
    // DOMAIN MODELS
    // =========================================================

    public class SaleLineItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string StaffName { get; set; } = "";
        public string StoreLocation { get; set; } = "";
        public DateTime SoldAt { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }

    public abstract class Promotion
    {
        public string Code { get; set; } = "";
    }

    public class PercentOffPromotion : Promotion
    {
        public double PercentOff { get; set; }
    }

    public class FlatAmountPromotion : Promotion
    {
        public decimal AmountOff { get; set; }
    }

    public class BuyOneGetOnePromotion : Promotion
    {
    }

    // =========================================================
    // RESULT CLASSES
    // =========================================================

    public class ProductSalesResult
    {
        public string ProductName { get; set; } = "";
        public int TotalQuantity { get; set; }
    }

    public class CategoryRevenueResult
    {
        public string Category { get; set; } = "";
        public decimal Revenue { get; set; }
    }

    public class StaffPerformance
    {
        public string StaffName { get; set; } = "";
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageSaleValue { get; set; }
    }

    public class HourlySalesResult
    {
        public int Hour { get; set; }
        public int SalesCount { get; set; }
        public decimal Revenue { get; set; }
    }

    public class StoreComparison
    {
        public string StoreLocation { get; set; } = "";
        public decimal Revenue { get; set; }
        public int ItemCount { get; set; }
        public string TopCategory { get; set; } = "";
    }

    // =========================================================
    // ANALYTICS ENGINE
    // =========================================================

    public class InsightDeskEngine
    {
        private readonly List<SaleLineItem> _sales;
        private readonly List<Promotion> _promotions;

        public InsightDeskEngine(
            List<SaleLineItem> sales,
            List<Promotion> promotions)
        {
            _sales = sales;
            _promotions = promotions;
        }

        // =====================================================
        // 1. TOP SELLING PRODUCTS
        // Method syntax
        // =====================================================

        /// <summary>
        /// Returns the top N products based on total quantity sold.
        /// </summary>
        public IEnumerable<ProductSalesResult> TopSellingProducts(int topN)
        {
            if (topN <= 0)
                return Enumerable.Empty<ProductSalesResult>();

            return _sales
                .GroupBy(s => s.ProductName)
                .Select(g => new ProductSalesResult
                {
                    ProductName = g.Key,
                    TotalQuantity = g.Sum(s => s.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(topN);
        }

        // =====================================================
        // 2. REVENUE BY CATEGORY
        // Query syntax
        // =====================================================

        /// <summary>
        /// Returns total revenue for each category in descending
        /// order of revenue.
        /// </summary>
        public IEnumerable<CategoryRevenueResult> RevenueByCategory()
        {
            var query =
                from sale in _sales
                group sale by sale.Category
                into categoryGroup
                let revenue = categoryGroup.Sum(s => s.LineTotal)
                orderby revenue descending
                select new CategoryRevenueResult
                {
                    Category = categoryGroup.Key,
                    Revenue = revenue
                };

            return query;
        }

        // =====================================================
        // 3. STAFF PERFORMANCE
        // Method syntax
        // =====================================================

        /// <summary>
        /// Returns sales count, revenue and average sale value
        /// for every staff member.
        /// </summary>
        public IEnumerable<StaffPerformance> StaffPerformanceReport()
        {
            return _sales
                .GroupBy(s => s.StaffName)
                .Select(g => new StaffPerformance
                {
                    StaffName = g.Key,
                    SalesCount = g.Count(),
                    TotalRevenue = g.Sum(s => s.LineTotal),
                    AverageSaleValue = g.Average(s => s.LineTotal)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ThenBy(x => x.StaffName);
        }

        // =====================================================
        // 4. HOURLY SALES TREND
        // Query syntax
        // =====================================================

        /// <summary>
        /// Groups sales by hour and returns sales count and
        /// revenue for each business hour.
        /// </summary>
        public IEnumerable<HourlySalesResult> HourlySalesTrend()
        {
            var query =
                from sale in _sales
                group sale by sale.SoldAt.Hour
                into hourGroup
                orderby hourGroup.Key
                select new HourlySalesResult
                {
                    Hour = hourGroup.Key,
                    SalesCount = hourGroup.Count(),
                    Revenue = hourGroup.Sum(s => s.LineTotal)
                };

            return query;
        }

        // =====================================================
        // 5. PERCENT OFF PROMOTIONS
        // Method syntax + OfType<T>
        // =====================================================

        /// <summary>
        /// Returns percent-off promotions whose percentage is
        /// greater than the supplied minimum percentage.
        /// </summary>
        public IEnumerable<PercentOffPromotion>
            PercentOffPromotionsOver(double minPercent)
        {
            return _promotions
                .OfType<PercentOffPromotion>()
                .Where(p => p.PercentOff > minPercent)
                .OrderByDescending(p => p.PercentOff);
        }

        // =====================================================
        // 6. LOW PERFORMING CATEGORIES
        // Query syntax with into + where
        // =====================================================

        /// <summary>
        /// Returns categories whose total revenue is below
        /// the specified threshold.
        /// </summary>
        public IEnumerable<CategoryRevenueResult>
            LowPerformingCategories(decimal revenueThreshold)
        {
            var query =
                from sale in _sales
                group sale by sale.Category
                into categoryGroup
                let revenue = categoryGroup.Sum(s => s.LineTotal)
                where revenue < revenueThreshold
                orderby revenue ascending
                select new CategoryRevenueResult
                {
                    Category = categoryGroup.Key,
                    Revenue = revenue
                };

            return query;
        }

        // =====================================================
        // 7. STORE COMPARISON
        // Method syntax
        // =====================================================

        /// <summary>
        /// Returns revenue, item count and highest-revenue category
        /// for every store.
        /// </summary>
        public IEnumerable<StoreComparison> StoreComparisonReport()
        {
            return _sales
                .GroupBy(s => s.StoreLocation)
                .Select(storeGroup =>
                {
                    var topCategory = storeGroup
                        .GroupBy(s => s.Category)
                        .Select(categoryGroup => new
                        {
                            Category = categoryGroup.Key,
                            Revenue = categoryGroup.Sum(s => s.LineTotal)
                        })
                        .OrderByDescending(x => x.Revenue)
                        .FirstOrDefault();

                    return new StoreComparison
                    {
                        StoreLocation = storeGroup.Key,
                        Revenue = storeGroup.Sum(s => s.LineTotal),
                        ItemCount = storeGroup.Sum(s => s.Quantity),
                        TopCategory = topCategory?.Category ?? "N/A"
                    };
                })
                .OrderByDescending(x => x.Revenue);
        }

        // =====================================================
        // 8. DEFERRED VS SNAPSHOT
        // =====================================================

        /// <summary>
        /// Demonstrates the difference between a deferred LINQ
        /// query and an immediately materialized snapshot.
        /// </summary>
        public void DeferredVsSnapshotDemo()
        {
            Console.WriteLine("\n--- Deferred vs Snapshot Demo ---");

            var deferredQuery = _sales
                .Where(s => s.Category == "Electronics");

            var snapshot = _sales
                .Where(s => s.Category == "Electronics")
                .ToList();

            Console.WriteLine(
                $"Before mutation - Deferred: {deferredQuery.Count()}");

            Console.WriteLine(
                $"Before mutation - Snapshot: {snapshot.Count}");

            // Mutate source collection
            _sales.Add(new SaleLineItem
            {
                Id = 999,
                ProductName = "New Headphones",
                Category = "Electronics",
                UnitPrice = 100,
                Quantity = 2,
                StaffName = "Alice",
                StoreLocation = "Downtown",
                SoldAt = new DateTime(2026, 8, 22, 18, 0, 0)
            });

            Console.WriteLine(
                $"After mutation - Deferred: {deferredQuery.Count()}");

            Console.WriteLine(
                $"After mutation - Snapshot: {snapshot.Count}");

            Console.WriteLine(
                "Deferred query sees the new item because it executes later.");

            Console.WriteLine(
                "Snapshot does not change because ToList() executed immediately.");
        }

        // =====================================================
        // QUERY/METHOD EQUIVALENCE CHECK
        // =====================================================

        /// <summary>
        /// Compares a query-syntax and method-syntax implementation
        /// of the same category revenue report.
        /// </summary>
        public void SyntaxEquivalenceCheck()
        {
            var querySyntax =
                from sale in _sales
                group sale by sale.Category
                into categoryGroup
                let revenue = categoryGroup.Sum(s => s.LineTotal)
                orderby revenue descending
                select new CategoryRevenueResult
                {
                    Category = categoryGroup.Key,
                    Revenue = revenue
                };

            var methodSyntax = _sales
                .GroupBy(s => s.Category)
                .Select(g => new CategoryRevenueResult
                {
                    Category = g.Key,
                    Revenue = g.Sum(s => s.LineTotal)
                })
                .OrderByDescending(x => x.Revenue);

            var result1 = querySyntax.ToList();
            var result2 = methodSyntax.ToList();

            bool identical =
                result1.Count == result2.Count &&
                result1.Zip(result2, (a, b) =>
                    a.Category == b.Category &&
                    a.Revenue == b.Revenue)
                .All(x => x);

            Console.WriteLine("\n--- Syntax Equivalence Check ---");
            Console.WriteLine(
                identical
                    ? "PASS: Both queries produce identical results."
                    : "FAIL: Results are different.");
        }

        // =====================================================
        // BROKEN ORDERBY DEMONSTRATION
        // =====================================================

        /// <summary>
        /// Demonstrates the incorrect use of OrderBy twice and
        /// compares it with the correct OrderBy followed by ThenBy.
        /// </summary>
        public void BrokenStaffSort()
        {
            var broken = _sales
                .GroupBy(s => s.StaffName)
                .Select(g => new StaffPerformance
                {
                    StaffName = g.Key,
                    SalesCount = g.Count(),
                    TotalRevenue = g.Sum(s => s.LineTotal),
                    AverageSaleValue = g.Average(s => s.LineTotal)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .OrderBy(x => x.StaffName);

            var correct = StaffPerformanceReport();

            Console.WriteLine("\n--- Broken Staff Sort ---");

            Console.WriteLine("BROKEN:");
            foreach (var staff in broken)
            {
                Console.WriteLine(
                    $"{staff.StaffName,-10} Revenue: {staff.TotalRevenue,10:C}");
            }

            Console.WriteLine("\nCORRECT:");
            foreach (var staff in correct)
            {
                Console.WriteLine(
                    $"{staff.StaffName,-10} Revenue: {staff.TotalRevenue,10:C}");
            }

            Console.WriteLine(
                "\nExplanation: OrderBy() starts a new primary sort. " +
                "ThenBy() should be used for the secondary sort.");
        }
    }

    // =========================================================
    // PROGRAM
    // =========================================================

    public class Program
    {
        static void Main()
        {
            // =================================================
            // 1. SEED SALES
            // =================================================

            var sales = SeedSales();

            // =================================================
            // 2. SEED PROMOTIONS
            // =================================================

            var promotions = SeedPromotions();

            var engine = new InsightDeskEngine(sales, promotions);

            Console.WriteLine("==========================================");
            Console.WriteLine("       INSIGHTDESK SALES ANALYTICS");
            Console.WriteLine("==========================================");

            // =================================================
            // 3. TOP PRODUCTS
            // =================================================

            Console.WriteLine("\n=== 1. TOP SELLING PRODUCTS ===");

            foreach (var item in engine.TopSellingProducts(5))
            {
                Console.WriteLine(
                    $"{item.ProductName,-20} Quantity: {item.TotalQuantity}");
            }

            // =================================================
            // 4. DEFERRED QUERY STORED
            // =================================================

            var revenueQuery = engine.RevenueByCategory();

            Console.WriteLine(
                "\nRevenue query created but not enumerated yet.");

            // Another operation before enumeration
            Console.WriteLine("\n=== STAFF PERFORMANCE ===");

            foreach (var staff in engine.StaffPerformanceReport())
            {
                Console.WriteLine(
                    $"{staff.StaffName,-10} " +
                    $"Count: {staff.SalesCount,-3} " +
                    $"Revenue: {staff.TotalRevenue,10:C} " +
                    $"Average: {staff.AverageSaleValue,10:C}");
            }

            Console.WriteLine("\n=== 2. REVENUE BY CATEGORY ===");

            foreach (var category in revenueQuery)
            {
                Console.WriteLine(
                    $"{category.Category,-15} {category.Revenue,12:C}");
            }

            // =================================================
            // 5. HOURLY QUERY STORED
            // =================================================

            var hourlyQuery = engine.HourlySalesTrend();

            Console.WriteLine(
                "\nHourly query created but not enumerated yet.");

            // Another operation
            Console.WriteLine("\n=== 5. PERCENT OFF PROMOTIONS ===");

            foreach (var promotion in
                     engine.PercentOffPromotionsOver(10))
            {
                Console.WriteLine(
                    $"{promotion.Code,-10} {promotion.PercentOff}% OFF");
            }

            Console.WriteLine("\n=== 4. HOURLY SALES TREND ===");

            foreach (var hour in hourlyQuery)
            {
                Console.WriteLine(
                    $"{hour.Hour:00}:00 " +
                    $"Count: {hour.SalesCount,-3} " +
                    $"Revenue: {hour.Revenue,10:C}");
            }

            // =================================================
            // 6. LOW PERFORMING CATEGORIES
            // =================================================

            Console.WriteLine("\n=== 6. LOW PERFORMING CATEGORIES ===");

            foreach (var category in
                     engine.LowPerformingCategories(1000))
            {
                Console.WriteLine(
                    $"{category.Category,-15} {category.Revenue,12:C}");
            }

            // =================================================
            // 7. STORE COMPARISON
            // =================================================

            Console.WriteLine("\n=== 7. STORE COMPARISON ===");

            foreach (var store in engine.StoreComparisonReport())
            {
                Console.WriteLine(
                    $"{store.StoreLocation,-12} " +
                    $"Revenue: {store.Revenue,10:C} " +
                    $"Items: {store.ItemCount,-3} " +
                    $"Top Category: {store.TopCategory}");
            }

            // =================================================
            // 8. SYNTAX EQUIVALENCE
            // =================================================

            engine.SyntaxEquivalenceCheck();

            // =================================================
            // 9. BROKEN VS CORRECT SORT
            // =================================================

            engine.BrokenStaffSort();

            // =================================================
            // 10. DEFERRED VS SNAPSHOT
            // =================================================

            engine.DeferredVsSnapshotDemo();

            // =================================================
            // 11. EDGE CASES
            // =================================================

            Console.WriteLine("\n=== EDGE CASES ===");

            Console.WriteLine("\nTop 100 products:");

            foreach (var product in engine.TopSellingProducts(100))
            {
                Console.WriteLine(
                    $"{product.ProductName,-20} " +
                    $"{product.TotalQuantity}");
            }

            Console.WriteLine(
                "\nPromotions above 999%:");

            var noPromotions =
                engine.PercentOffPromotionsOver(999);

            if (!noPromotions.Any())
            {
                Console.WriteLine("No matching promotions found.");
            }

            Console.WriteLine("\nProgram completed successfully.");
        }

        // =====================================================
        // SALES SEED DATA
        // =====================================================

        static List<SaleLineItem> SeedSales()
        {
            var sales = new List<SaleLineItem>();

            string[] products =
            {
                "Laptop",
                "Phone",
                "Headphones",
                "Keyboard",
                "Mouse",
                "Monitor",
                "Jeans",
                "T-Shirt",
                "Jacket",
                "Shoes",
                "Rice",
                "Milk",
                "Coffee",
                "Biscuits",
                "Apple",
                "Banana",
                "Novel",
                "Notebook",
                "Pen",
                "Backpack"
            };

            string[] categories =
            {
                "Electronics",
                "Clothing",
                "Grocery",
                "Stationery"
            };

            string[] staff =
            {
                "Alice",
                "Bob",
                "Charlie"
            };

            string[] stores =
            {
                "Downtown",
                "Mall"
            };

            decimal[] prices =
            {
                800, 500, 100, 70, 40,
                300, 120, 30, 250, 180,
                60, 40, 80, 25, 10,
                15, 200, 50, 10, 150
            };

            for (int i = 0; i < 40; i++)
            {
                int productIndex = i % products.Length;

                sales.Add(new SaleLineItem
                {
                    Id = i + 1,
                    ProductName = products[productIndex],

                    Category = categories[
                        productIndex / 5],

                    UnitPrice = prices[productIndex],

                    Quantity = (i % 5) + 1,

                    StaffName = staff[i % staff.Length],

                    StoreLocation =
                        stores[i % stores.Length],

                    SoldAt = new DateTime(
                        2026,
                        8,
                        22,
                        9 + (i % 10),
                        (i * 7) % 60,
                        0)
                });
            }

            return sales;
        }

        // =====================================================
        // PROMOTION SEED DATA
        // =====================================================

        static List<Promotion> SeedPromotions()
        {
            return new List<Promotion>
            {
                new PercentOffPromotion
                {
                    Code = "P10",
                    PercentOff = 10
                },

                new PercentOffPromotion
                {
                    Code = "P15",
                    PercentOff = 15
                },

                new PercentOffPromotion
                {
                    Code = "P20",
                    PercentOff = 20
                },

                new FlatAmountPromotion
                {
                    Code = "F50",
                    AmountOff = 50
                },

                new FlatAmountPromotion
                {
                    Code = "F100",
                    AmountOff = 100
                },

                new BuyOneGetOnePromotion
                {
                    Code = "BOGO"
                }
            };
        }
    }
}