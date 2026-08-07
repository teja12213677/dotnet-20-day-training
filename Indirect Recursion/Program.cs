using System;

class Program
{
    static bool IsPositiveChain(int n)
    {
        if (n == 0)
            return true;

        if (n > 0)
            return IsNegativeChain(n - 1);
        else
            return IsNegativeChain(n + 1);
    }

    static bool IsNegativeChain(int n)
    {
        if (n == 0)
            return true;

        if (n > 0)
            return IsPositiveChain(n - 1);
        else
            return IsPositiveChain(n + 1);
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        bool result;

        if (number >= 0)
            result = IsPositiveChain(number);
        else
            result = IsNegativeChain(number);

        Console.WriteLine(result);
    }
}