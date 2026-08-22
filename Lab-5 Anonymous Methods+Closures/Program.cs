using System;

class Lab5
{
    static void Main()
    {
        Console.WriteLine("======================================");
        Console.WriteLine(" Lab 5 - Anonymous Methods + Closures");
        Console.WriteLine("======================================");


        // ============================================================
        // 1. Anonymous method using the delegate keyword
        // ============================================================

        Console.WriteLine("\n1. Anonymous Method - Square");
        Console.WriteLine("-----------------------------");

        Action<int> squareAnonymous = delegate (int number)
        {
            int square = number * number;
            Console.WriteLine($"Square of {number} = {square}");
        };

        squareAnonymous(5);
        squareAnonymous(8);


        // ============================================================
        // 2. Anonymous method with a closure
        // ============================================================

        Console.WriteLine("\n2. Anonymous Method - Closure");
        Console.WriteLine("------------------------------");

        int total = 0;

        Action addAnonymous = delegate
        {
            total++;
            Console.WriteLine($"Total inside anonymous method = {total}");
        };

        Console.WriteLine("Calling anonymous method 5 times:");

        for (int i = 0; i < 5; i++)
        {
            addAnonymous();
        }

        Console.WriteLine(
            $"Total after anonymous method calls = {total}");


        // ============================================================
        // 3. Lambda version of the square method
        // ============================================================

        Console.WriteLine("\n3. Lambda Method - Square");
        Console.WriteLine("--------------------------");

        Action<int> squareLambda = number =>
        {
            int square = number * number;
            Console.WriteLine($"Square of {number} = {square}");
        };

        squareLambda(5);
        squareLambda(8);


        // ============================================================
        // 4. Lambda version of the closure
        // ============================================================

        Console.WriteLine("\n4. Lambda Method - Closure");
        Console.WriteLine("---------------------------");

        int lambdaTotal = 0;

        Action addLambda = () =>
        {
            lambdaTotal++;
            Console.WriteLine(
                $"Total inside lambda = {lambdaTotal}");
        };

        Console.WriteLine("Calling lambda 5 times:");

        for (int i = 0; i < 5; i++)
        {
            addLambda();
        }

        Console.WriteLine(
            $"Total after lambda calls = {lambdaTotal}");


        // ============================================================
        // Comparison
        // ============================================================

        /*
         Anonymous method:
             delegate (int number) { ... }

         Lambda:
             number => { ... }

         Both create delegates and can capture outer variables.
         The main difference is syntax: lambdas provide a shorter
         and more concise syntax than the delegate keyword.
        */

        Console.WriteLine("\n======================================");
        Console.WriteLine(" Comparison");
        Console.WriteLine("======================================");

        Console.WriteLine(
            $"Anonymous method final total = {total}");

        Console.WriteLine(
            $"Lambda final total           = {lambdaTotal}");

        if (total == lambdaTotal)
        {
            Console.WriteLine(
                "Both closure versions produced the same result.");
        }

        Console.WriteLine("\nProgram completed successfully.");
    }
}