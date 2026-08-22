using System;

class Lab1
{
    static void Main()
    {
        // ============================================================
        // 1. var vs Explicit Type vs dynamic
        // ============================================================

        var count = 10;
        int countExplicit = 10;
        dynamic countDynamic = 10;

        Console.WriteLine("1. var vs Explicit Type vs dynamic");
        Console.WriteLine("-----------------------------------");

        Console.WriteLine($"var count = {count}");
        Console.WriteLine($"count.GetType() = {count.GetType()}");

        Console.WriteLine();

        Console.WriteLine($"int countExplicit = {countExplicit}");
        Console.WriteLine(
            $"countExplicit.GetType() = {countExplicit.GetType()}");

        Console.WriteLine();

        Console.WriteLine($"dynamic countDynamic = {countDynamic}");
        Console.WriteLine(
            $"countDynamic.GetType() = {countDynamic.GetType()}");


        // ============================================================
        // 2. dynamic runtime type change and runtime exception
        // ============================================================

        Console.WriteLine("\n2. dynamic Runtime Exception");
        Console.WriteLine("----------------------------");

        countDynamic = "now text";

        Console.WriteLine($"countDynamic = {countDynamic}");
        Console.WriteLine($"Runtime type = {countDynamic.GetType()}");

        try
        {
            // The compiler allows this because countDynamic is dynamic.
            // At runtime, the value is a string, so adding an int
            // causes a RuntimeBinderException.
            var result = countDynamic + 5;

            Console.WriteLine($"Result = {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Runtime exception caught!");
            Console.WriteLine($"Exception Type: {ex.GetType().Name}");
            Console.WriteLine($"Message: {ex.Message}");
        }


        // ============================================================
        // 3. Anonymous Type
        // ============================================================

        Console.WriteLine("\n3. Anonymous Type");
        Console.WriteLine("-----------------");

        var point = new
        {
            X = 3,
            Y = 7
        };

        Console.WriteLine($"X = {point.X}");
        Console.WriteLine($"Y = {point.Y}");

        // point.X = 10;
        // Compiler error:
        // CS0200: Property or indexer '<anonymous type>.X'
        // cannot be assigned to -- it is read only.


        // ============================================================
        // 4. var vs dynamic explanation
        // ============================================================

        /*
         When would I choose dynamic over var?

         I would normally choose var when the type is known at compile time
         because var provides compile-time type safety while keeping the code
         concise. I would choose dynamic only when I genuinely need runtime
         binding, such as when working with COM objects, reflection-based APIs,
         or data whose members/types cannot conveniently be known at compile
         time. For example, a legacy COM API may return objects whose members
         need to be resolved at runtime. In such a scenario, dynamic can make
         the code easier to work with, but it gives up compile-time checking
         and can cause runtime exceptions.
        */

        Console.WriteLine("\n4. Explanation");
        Console.WriteLine("---------------");
        Console.WriteLine(
            "var is preferred when the type is known at compile time.");
        Console.WriteLine(
            "dynamic is useful when runtime binding is genuinely required.");
    }
}