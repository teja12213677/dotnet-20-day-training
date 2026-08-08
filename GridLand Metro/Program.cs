using System;
using System.Collections.Generic;

class Program
{
    static long GridlandMetro(int n, int m, int k, List<int[]> tracks)
    {
        // Group tracks by row
        Dictionary<int, List<int[]>> rows =
            new Dictionary<int, List<int[]>>();

        foreach (int[] track in tracks)
        {
            int row = track[0];
            int start = track[1];
            int end = track[2];

            if (!rows.ContainsKey(row))
            {
                rows[row] = new List<int[]>();
            }

            rows[row].Add(new int[] { start, end });
        }

        // Number of cells covered by tracks
        long coveredCells = 0;

        // Process each row
        foreach (var row in rows)
        {
            List<int[]> intervals = row.Value;

            // Sort according to starting column
            intervals.Sort((a, b) => a[0].CompareTo(b[0]));

            int currentStart = intervals[0][0];
            int currentEnd = intervals[0][1];

            // Merge overlapping tracks
            for (int i = 1; i < intervals.Count; i++)
            {
                int nextStart = intervals[i][0];
                int nextEnd = intervals[i][1];

                if (nextStart <= currentEnd)
                {
                    // Overlapping tracks
                    currentEnd = Math.Max(currentEnd, nextEnd);
                }
                else
                {
                    // No overlap
                    coveredCells +=
                        (long)currentEnd - currentStart + 1;

                    currentStart = nextStart;
                    currentEnd = nextEnd;
                }
            }

            // Add the last interval
            coveredCells +=
                (long)currentEnd - currentStart + 1;
        }

        // Total cells in the grid
        long totalCells = (long)n * m;

        // Cells where lampposts can be placed
        return totalCells - coveredCells;
    }

    static void Main(string[] args)
    {
        Console.Write("Enter n, m and k: ");

        string[] first =
            Console.ReadLine().Split(' ');

        int n = int.Parse(first[0]);
        int m = int.Parse(first[1]);
        int k = int.Parse(first[2]);

        List<int[]> tracks = new List<int[]>();

        Console.WriteLine("Enter the tracks:");

        for (int i = 0; i < k; i++)
        {
            string[] input =
                Console.ReadLine().Split(' ');

            int row = int.Parse(input[0]);
            int start = int.Parse(input[1]);
            int end = int.Parse(input[2]);

            tracks.Add(new int[] { row, start, end });
        }

        long result =
            GridlandMetro(n, m, k, tracks);

        Console.WriteLine();
        Console.WriteLine("Number of lampposts: " + result);

        Console.ReadLine();
    }
}