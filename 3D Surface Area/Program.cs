using System;
using System.Collections.Generic;

class Program
{
    static int SurfaceArea(List<List<int>> A)
    {
        int H = A.Count;
        int W = A[0].Count;

        int area = 0;

        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                int height = A[i][j];

                if (height > 0)
                {
                    area += 2;
                }

                if (i == 0)
                {
                    area += height;
                }
                else
                {
                    area += Math.Max(0, height - A[i - 1][j]);
                }

                if (i == H - 1)
                {
                    area += height;
                }
                else
                {
                    area += Math.Max(0, height - A[i + 1][j]);
                }

                if (j == 0)
                {
                    area += height;
                }
                else
                {
                    area += Math.Max(0, height - A[i][j - 1]);
                }

                if (j == W - 1)
                {
                    area += height;
                }
                else
                {
                    area += Math.Max(0, height - A[i][j + 1]);
                }
            }
        }

        return area;
    }

    static void Main()
    {
       
        string[] first = Console.ReadLine().Split(' ');

        int H = int.Parse(first[0]);
        int W = int.Parse(first[1]);

        List<List<int>> A = new List<List<int>>();

        for (int i = 0; i < H; i++)
        {
            string[] input = Console.ReadLine().Split(' ');

            List<int> row = new List<int>();

            for (int j = 0; j < W; j++)
            {
                row.Add(int.Parse(input[j]));
            }

            A.Add(row);
        }
        int result = SurfaceArea(A);

        Console.WriteLine("Surface Area = " + result);
    }
}