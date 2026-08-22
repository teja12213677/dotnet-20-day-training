using System;
using System.Collections.Generic;

class Lab8
{
    // ============================================================
    // 1. Generic callback-driven ProcessBatch method
    // ============================================================

    public static void ProcessBatch<T>(
        List<T> items,
        Action<T> onSuccess,
        Action<T, string> onFailure,
        Func<T, bool> validator)
    {
        foreach (T item in items)
        {
            // Validate the current item
            if (validator(item))
            {
                // Validation passed
                onSuccess(item);
            }
            else
            {
                // Validation failed
                string reason = "Item failed validation.";
                onFailure(item, reason);
            }
        }
    }


    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine(" Lab 8 - Delegates as Callback Parameters");
        Console.WriteLine("==========================================");


        // ============================================================
        // 2. Process List<int>
        //    Validator rejects negative numbers
        // ============================================================

        Console.WriteLine("\n1. Processing Integers");
        Console.WriteLine("----------------------");

        List<int> numbers = new List<int>
        {
            10,
            -5,
            20,
            -15,
            30
        };

        ProcessBatch(
            numbers,

            // onSuccess
            number =>
            {
                Console.WriteLine(
                    $"SUCCESS: {number} is a valid number.");
            },

            // onFailure
            (number, reason) =>
            {
                Console.WriteLine(
                    $"FAILURE: {number} rejected. Reason: Negative number.");
            },

            // validator
            number => number >= 0
        );


        // ============================================================
        // 3. Process List<string>
        //    Validator rejects empty/whitespace strings
        // ============================================================

        Console.WriteLine("\n2. Processing Strings");
        Console.WriteLine("---------------------");

        List<string> names = new List<string>
        {
            "Teja",
            "",
            "Capgemini",
            "   ",
            "CSharp"
        };

        ProcessBatch(
            names,

            // onSuccess
            name =>
            {
                Console.WriteLine(
                    $"SUCCESS: '{name}' is a valid string.");
            },

            // onFailure
            (name, reason) =>
            {
                Console.WriteLine(
                    $"FAILURE: Empty/whitespace string rejected.");
            },

            // validator
            name => !string.IsNullOrWhiteSpace(name)
        );


        Console.WriteLine("\n==========================================");
        Console.WriteLine(" Program completed successfully.");
        Console.WriteLine("==========================================");
    }
}