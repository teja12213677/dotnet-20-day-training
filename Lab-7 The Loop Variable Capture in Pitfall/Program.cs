using System;
using System.Collections.Generic;

class Lab7
{
    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine(" Lab 7 - Loop Variable Capture Pitfall");
        Console.WriteLine("==========================================");


        // ============================================================
        // 1. BUGGY for-loop version
        // ============================================================

        Console.WriteLine("\n1. BUGGY for-loop Version");
        Console.WriteLine("--------------------------");

        List<Action> buggyActions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            // The lambda captures the variable 'i', not its current value.
            buggyActions.Add(() =>
            {
                Console.WriteLine($"Captured index: {i}");
            });
        }

        Console.WriteLine("Invoking delegates AFTER the loop:");

        foreach (Action action in buggyActions)
        {
            action();
        }

        /*
         Expected output:

         Captured index: 3
         Captured index: 3
         Captured index: 3

         Why?

         All three lambdas capture the same 'i' variable.
         The delegates are not executed inside the loop.
         They are executed after the loop has finished.

         After the loop:
             i = 3

         Therefore, all three delegates see the final value 3.
        */


        // ============================================================
        // 2. FIXED for-loop version
        // ============================================================

        Console.WriteLine("\n2. FIXED for-loop Version");
        Console.WriteLine("--------------------------");

        List<Action> fixedActions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            // Create a new local variable for each loop iteration.
            int capturedIndex = i;

            fixedActions.Add(() =>
            {
                Console.WriteLine(
                    $"Captured index: {capturedIndex}");
            });
        }

        Console.WriteLine("Invoking delegates AFTER the loop:");

        foreach (Action action in fixedActions)
        {
            action();
        }

        /*
         Expected output:

         Captured index: 0
         Captured index: 1
         Captured index: 2

         Each iteration creates a separate 'capturedIndex' variable.

         Delegate 1 captures capturedIndex = 0
         Delegate 2 captures capturedIndex = 1
         Delegate 3 captures capturedIndex = 2
        */


        // ============================================================
        // 3. foreach version WITHOUT manual copying
        // ============================================================

        Console.WriteLine("\n3. foreach Version");
        Console.WriteLine("------------------");

        List<int> numbers = new List<int> { 0, 1, 2 };

        List<Action> foreachActions = new List<Action>();

        foreach (int number in numbers)
        {
            // No manual copy is required here.
            foreachActions.Add(() =>
            {
                Console.WriteLine(
                    $"Captured number: {number}");
            });
        }

        Console.WriteLine("Invoking delegates AFTER the foreach loop:");

        foreach (Action action in foreachActions)
        {
            action();
        }

        /*
         Expected output:

         Captured number: 0
         Captured number: 1
         Captured number: 2

         Why is foreach different?

         In modern C#, the iteration variable of a foreach loop
         is treated as a separate variable for each iteration when
         captured by a lambda.

         Therefore, each lambda captures its own iteration variable.

         This differs from the traditional for-loop problem where
         all lambdas capture the same loop variable 'i'.
        */


        // ============================================================
        // Summary
        // ============================================================

        Console.WriteLine("\n==========================================");
        Console.WriteLine(" Summary");
        Console.WriteLine("==========================================");

        Console.WriteLine(
            "Buggy for:  3 3 3");

        Console.WriteLine(
            "Fixed for:  0 1 2");

        Console.WriteLine(
            "foreach:    0 1 2");

        Console.WriteLine("\nProgram completed successfully.");
    }
}