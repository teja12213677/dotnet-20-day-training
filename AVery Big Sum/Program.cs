using System;
using System.Collections.Generic;

class Program
{
    static long aVeryBigSum(List<long> ar)
    {
        long sum = 0;

        foreach (long number in ar)
        {
            sum += number;
        }

        return sum;
    }

    static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());

        string[] input = Console.ReadLine().Split(' ');

        List<long> numbers = new List<long>();

        foreach (string value in input)
        {
            numbers.Add(Convert.ToInt64(value));
        }

        long result = aVeryBigSum(numbers);

        Console.WriteLine(result);
    }
}