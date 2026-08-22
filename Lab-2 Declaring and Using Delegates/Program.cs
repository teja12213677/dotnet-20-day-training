using System;
using System.Collections.Generic;

class Lab2
{
    // 1. Declare a custom delegate
    public delegate double Discount(double price);

    // 2. Three methods matching the delegate signature

    public static double NoDiscount(double price)
    {
        return price;
    }

    public static double TenPercentOff(double price)
    {
        return price * 0.90;
    }

    public static double HalfOff(double price)
    {
        return price * 0.50;
    }

    // 3. Method that accepts and invokes a delegate
    public static double ApplyDiscount(double price, Discount discount)
    {
        return discount(price);
    }

    static void Main()
    {
        double price = 1000.00;

        Console.WriteLine("=== Lab 2: Declaring and Using Delegates ===");
        Console.WriteLine($"Original Price: {price:C}");
        Console.WriteLine();

        // ------------------------------------------------------------
        // 4. Direct delegate calls
        // ------------------------------------------------------------

        Console.WriteLine("1. Direct Delegate Calls");
        Console.WriteLine("------------------------");

        // Instantiate delegate with NoDiscount
        Discount noDiscount = NoDiscount;

        // Instantiate delegate with TenPercentOff
        Discount tenPercentOff = TenPercentOff;

        // Instantiate delegate with HalfOff
        Discount halfOff = HalfOff;

        Console.WriteLine(
            $"No Discount: {ApplyDiscount(price, noDiscount):C}");

        Console.WriteLine(
            $"10% Off:     {ApplyDiscount(price, tenPercentOff):C}");

        Console.WriteLine(
            $"50% Off:     {ApplyDiscount(price, halfOff):C}");

        Console.WriteLine();


        // ------------------------------------------------------------
        // 5. Store all delegates in a List<Discount>
        // ------------------------------------------------------------

        Console.WriteLine("2. Delegates Stored in List<Discount>");
        Console.WriteLine("------------------------------------");

        List<Discount> discounts = new List<Discount>
        {
            NoDiscount,
            TenPercentOff,
            HalfOff
        };

        foreach (Discount discount in discounts)
        {
            double result = discount(price);

            Console.WriteLine($"Discount Result: {result:C}");
        }

        Console.WriteLine();

        Console.WriteLine("Program completed successfully.");
    }
}