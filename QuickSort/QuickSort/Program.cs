using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int[] arr = { 64, 25, 12, 22, 11 };

        Console.WriteLine("Original Array:");
        PrintArray(arr);

        long beforeMemory = GC.GetTotalMemory(true);

        Stopwatch stopwatch = Stopwatch.StartNew();

        QuickSort(arr, 0, arr.Length - 1);

        stopwatch.Stop();

        long afterMemory = GC.GetTotalMemory(true);

        Console.WriteLine("\nSorted Array:");
        PrintArray(arr);

        Console.WriteLine($"\nTimed run: {arr.Length} elements");
        Console.WriteLine($"Elapsed: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");
        Console.WriteLine($"Allocated: {afterMemory - beforeMemory} bytes");
        Console.WriteLine($"Valid sort: {IsSorted(arr)}");
    }

    static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(arr, low, high);

            QuickSort(arr, low, pivot - 1);
            QuickSort(arr, pivot + 1, high);
        }
    }

    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }

    static void PrintArray(int[] arr)
    {
        foreach (int item in arr)
            Console.Write(item + " ");
        Console.WriteLine();
    }

    static bool IsSorted(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < arr[i - 1])
                return false;
        }
        return true;
    }
}