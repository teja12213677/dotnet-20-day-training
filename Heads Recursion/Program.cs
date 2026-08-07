using System;

class Program
{
    // Head Recursive Method
    static void SumDigitsReversed(int n)
    {
        // Base Case
        if (n == 0)
            return;

        // Recursive Call (Head Recursion)
        SumDigitsReversed(n / 10);

        // Processing after recursion
        Console.Write(n % 10 + " ");
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = int.Parse(Console.ReadLine());

        Console.Write("Digits in reverse order: ");
        SumDigitsReversed(number);

        Console.ReadLine();
    }
}