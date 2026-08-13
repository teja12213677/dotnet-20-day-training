using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] arr = { 11, 12, 22, 25, 33, 45, 64, 90 };
        int key = 45;

        Console.WriteLine("Sorted Array:");
        PrintArray(arr);
        Console.WriteLine("Searching for: " + key);

        long beforeMemory = GC.GetTotalMemory(true);

        Stopwatch stopwatch = Stopwatch.StartNew();

        int index = JumpSearch(arr, key);

        stopwatch.Stop();

        long afterMemory = GC.GetTotalMemory(true);

        if (index != -1)
            Console.WriteLine($"\nElement found at index {index}");
        else
            Console.WriteLine("\nElement not found");

        Console.WriteLine($"\nTimed run: {arr.Length} elements");
        Console.WriteLine($"Elapsed: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");
        Console.WriteLine($"Allocated: {afterMemory - beforeMemory} bytes");
    }

    static int JumpSearch(int[] arr, int key)
    {
        int n = arr.Length;
        int step = (int)Math.Sqrt(n);
        int prev = 0;

        // Jump ahead until the block containing the key is found
        while (prev < n && arr[Math.Min(step, n) - 1] < key)
        {
            prev = step;
            step += (int)Math.Sqrt(n);

            if (prev >= n)
                return -1;
        }

        // Linear search within the block
        while (prev < Math.Min(step, n))
        {
            if (arr[prev] == key)
                return prev;

            prev++;
        }

        return -1;
    }

    static void PrintArray(int[] arr)
    {
        foreach (int item in arr)
            Console.Write(item + " ");
        Console.WriteLine();
    }
}