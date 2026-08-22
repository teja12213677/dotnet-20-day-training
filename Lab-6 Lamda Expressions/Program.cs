using System;
using System.Collections.Generic;

class Lab6
{
    // Product class
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double DiscountPercent { get; set; }
        public int Stock { get; set; }

        public Product(
            string name,
            double price,
            double discountPercent,
            int stock)
        {
            Name = name;
            Price = price;
            DiscountPercent = discountPercent;
            Stock = stock;
        }

        // Computed discounted price
        public double DiscountedPrice =>
            Price - (Price * DiscountPercent / 100);
    }

    // Order class
    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public List<Product> Products { get; set; }

        public Order(
            string orderId,
            string customerName,
            List<Product> products)
        {
            OrderId = orderId;
            CustomerName = customerName;
            Products = products;
        }
    }

    // Helper method to print products
    static void PrintProducts(List<Product> products)
    {
        foreach (Product product in products)
        {
            Console.WriteLine(
                $"Name: {product.Name,-12} " +
                $"Price: {product.Price,8:C} " +
                $"Discount: {product.DiscountPercent,5}% " +
                $"Stock: {product.Stock}");
        }
    }

    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine(" Lab 6 - Lambda Expressions");
        Console.WriteLine("==========================================");


        // ============================================================
        // 1. Expression-bodied lambda
        // ============================================================

        Console.WriteLine("\n1. Expression-Bodied Lambda");
        Console.WriteLine("----------------------------");

        Func<double, double, double> rectangleArea =
            (w, h) => w * h;

        double width = 10;
        double height = 5;

        double area = rectangleArea(width, height);

        Console.WriteLine(
            $"Rectangle: Width = {width}, Height = {height}");

        Console.WriteLine($"Area = {area}");


        // ============================================================
        // 2. Statement-bodied lambda
        // ============================================================

        Console.WriteLine("\n2. Statement-Bodied Lambda");
        Console.WriteLine("---------------------------");

        Action<Order> printReceipt = order =>
        {
            // Multiple statements are allowed inside { }
            Console.WriteLine("----------------------------------");
            Console.WriteLine("           ORDER RECEIPT");
            Console.WriteLine("----------------------------------");

            Console.WriteLine($"Order ID : {order.OrderId}");
            Console.WriteLine($"Customer : {order.CustomerName}");

            Console.WriteLine("----------------------------------");
            Console.WriteLine("Items:");

            double total = 0;

            foreach (Product product in order.Products)
            {
                Console.WriteLine(
                    $"{product.Name,-15} {product.Price,8:C}");

                total += product.Price;
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Total    : {total:C}");
            Console.WriteLine("----------------------------------");
        };

        List<Product> orderProducts = new List<Product>
        {
            new Product("Keyboard", 1500, 10, 5),
            new Product("Mouse", 800, 5, 10),
            new Product("Monitor", 12000, 15, 3)
        };

        Order order = new Order(
            "ORD-1001",
            "Teja",
            orderProducts);

        printReceipt(order);


        // ============================================================
        // 3. Sort List<Product> in three different ways
        // ============================================================

        Console.WriteLine("\n3. Lambda-Based Sorting");
        Console.WriteLine("------------------------");

        List<Product> products = new List<Product>
        {
            new Product("Laptop", 60000, 10, 5),
            new Product("Mouse", 800, 5, 10),
            new Product("Keyboard", 1500, 20, 0),
            new Product("Monitor", 12000, 15, 3),
            new Product("Headphones", 3000, 25, 0)
        };


        // ------------------------------------------------------------
        // 3a. Price ascending
        // ------------------------------------------------------------

        Console.WriteLine("\nOriginal Product List:");
        PrintProducts(products);

        products.Sort((p1, p2) =>
            p1.Price.CompareTo(p2.Price));

        Console.WriteLine("\nSorted by Price - Ascending:");
        PrintProducts(products);


        // ------------------------------------------------------------
        // 3b. Name descending
        // ------------------------------------------------------------

        products.Sort((p1, p2) =>
            string.Compare(
                p2.Name,
                p1.Name,
                StringComparison.Ordinal));

        Console.WriteLine("\nSorted by Name - Descending:");
        PrintProducts(products);


        // ------------------------------------------------------------
        // 3c. Discounted price
        // ------------------------------------------------------------

        products.Sort((p1, p2) =>
            p1.DiscountedPrice.CompareTo(
                p2.DiscountedPrice));

        Console.WriteLine(
            "\nSorted by Discounted Price - Ascending:");

        PrintProducts(products);


        // ============================================================
        // 4. RemoveAll with Predicate<T>
        // ============================================================

        Console.WriteLine("\n4. RemoveAll - Out-of-Stock Products");
        Console.WriteLine("-------------------------------------");

        List<Product> inventory = new List<Product>
        {
            new Product("Laptop", 60000, 10, 5),
            new Product("Mouse", 800, 5, 10),
            new Product("Keyboard", 1500, 20, 0),
            new Product("Monitor", 12000, 15, 3),
            new Product("Headphones", 3000, 25, 0)
        };

        Console.WriteLine("Before RemoveAll:");
        PrintProducts(inventory);

        // Remove every product whose stock is zero
        int removedCount = inventory.RemoveAll(
            product => product.Stock == 0);

        Console.WriteLine(
            $"\nRemoved products: {removedCount}");

        Console.WriteLine("\nAfter RemoveAll:");
        PrintProducts(inventory);


        Console.WriteLine("\n==========================================");
        Console.WriteLine(" Program completed successfully.");
        Console.WriteLine("==========================================");
    }
}