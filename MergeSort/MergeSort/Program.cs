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

        MergeSort(arr, 0, arr.Length - 1);

        stopwatch.Stop();

        long afterMemory = GC.GetTotalMemory(true);

        Console.WriteLine("\nSorted Array:");
        PrintArray(arr);

        Console.WriteLine($"\nTimed run: {arr.Length} elements");
        Console.WriteLine($"Elapsed: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");
        Console.WriteLine($"Allocated: {afterMemory - beforeMemory} bytes");
        Console.WriteLine($"Valid sort: {IsSorted(arr)}");
    }

    static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;

            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);

            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++)
            L[i] = arr[left + i];

        for (int j = 0; j < n2; j++)
            R[j] = arr[mid + 1 + j];

        int x = 0, y = 0, k = left;

        while (x < n1 && y < n2)
        {
            if (L[x] <= R[y])
                arr[k++] = L[x++];
            else
                arr[k++] = R[y++];
        }

        while (x < n1)
            arr[k++] = L[x++];

        while (y < n2)
            arr[k++] = R[y++];
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