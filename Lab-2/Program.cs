using System;
using System.Diagnostics;
using System.Text;

class Program
{
   
    static string BuildWithString(int count)
    {
        string result = "";

        for (int i = 0; i < count; i++)
        {
            result += i.ToString();
        }

        return result;
    }

    static string BuildWithStringBuilder(int count)
    {
        StringBuilder result = new StringBuilder(count * 5);

        for (int i = 0; i < count; i++)
        {
            result.Append(i.ToString());
        }

        return result.ToString();
    }

    static void Main()
    {
        int count = 50000;

        Stopwatch stopwatch1 = Stopwatch.StartNew();

        BuildWithString(count);

        stopwatch1.Stop();

        Stopwatch stopwatch2 = Stopwatch.StartNew();

        BuildWithStringBuilder(count);

        stopwatch2.Stop();

        long stringTime = stopwatch1.ElapsedMilliseconds;
        long stringBuilderTime = stopwatch2.ElapsedMilliseconds;

        Console.WriteLine(
            "String concatenation (50,000 items): "
            + stringTime + " ms");

        Console.WriteLine(
            "StringBuilder (50,000 items): "
            + stringBuilderTime + " ms");

        if (stringBuilderTime > 0)
        {
            double ratio =
                (double)stringTime / stringBuilderTime;

            Console.WriteLine(
                "Ratio (String / StringBuilder): "
                + ratio.ToString("F2") + "x");
        }

        Console.ReadLine();
    }
}