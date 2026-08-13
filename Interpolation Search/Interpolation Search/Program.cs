using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] arr = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        int key = 60;

        Console.WriteLine("Sorted Array:");
        PrintArray(arr);
        Console.WriteLine("Searching for: " + key);

        long beforeMemory = GC.GetTotalMemory(true);

        Stopwatch stopwatch = Stopwatch.StartNew();

        int index = InterpolationSearch(arr, key);

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

    static int InterpolationSearch(int[] arr, int key)
    {
        int low = 0;
        int high = arr.Length - 1;

        while (low <= high &&
               key >= arr[low] &&
               key <= arr[high])
        {
            if (low == high)
            {
                if (arr[low] == key)
                    return low;
                return -1;
            }

            int pos = low + ((key - arr[low]) * (high - low))
                      / (arr[high] - arr[low]);

            if (arr[pos] == key)
                return pos;

            if (arr[pos] < key)
                low = pos + 1;
            else
                high = pos - 1;
        }

        return -1;
    }

    static void PrintArray(int[] arr)
    {
        foreach (int item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}