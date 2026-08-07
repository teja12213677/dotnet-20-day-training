using System;

class Program
{
  
    static int Factorial(int n, int accumulator = 1)
    {
      
        if (n == 0 || n == 1)
            return accumulator;

        return Factorial(n - 1, accumulator * n);
    }

    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int result = Factorial(number);

        Console.WriteLine("Factorial = " + result);
    }
}