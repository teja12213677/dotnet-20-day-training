using System;

class Program
{
    // ---------------- Head Recursion ----------------
    static void HeadRecursion(int n)
    {
        if (n == 0)
            return;

        HeadRecursion(n - 1);

        Console.WriteLine(n);
    }

    // ---------------- Tail Recursion ----------------
    static void TailRecursion(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(n);

        TailRecursion(n - 1);
    }

    // ---------------- Tree Recursion ----------------
    static void TreeRecursion(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(n);

        TreeRecursion(n - 1);

        TreeRecursion(n - 1);
    }

    // ---------------- Indirect Recursion ----------------
    static void MethodX(int n)
    {
        if (n <= 0)
            return;

        Console.WriteLine("MethodX : " + n);

        MethodY(n - 1);
    }

    static void MethodY(int n)
    {
        if (n <= 0)
            return;

        Console.WriteLine("MethodY : " + n);

        MethodX(n - 1);
    }

    // ---------------- Main Method ----------------
    static void Main()
    {
        Console.WriteLine("===== Recursion Patterns =====");
        Console.WriteLine("1. Head Recursion");
        Console.WriteLine("2. Tail Recursion");
        Console.WriteLine("3. Tree Recursion");
        Console.WriteLine("4. Indirect Recursion");

        Console.Write("\nEnter your choice (1-4): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter a number: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();

        switch (choice)
        {
            case 1:
                Console.WriteLine("Head Recursion Output:");
                HeadRecursion(n);
                break;

            case 2:
                Console.WriteLine("Tail Recursion Output:");
                TailRecursion(n);
                break;

            case 3:
                Console.WriteLine("Tree Recursion Output:");
                TreeRecursion(n);
                break;

            case 4:
                Console.WriteLine("Indirect Recursion Output:");
                MethodX(n);
                break;

            default:
                Console.WriteLine("Invalid Choice!");
                break;
        }
    }
}