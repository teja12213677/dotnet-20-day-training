using System;
using System.Collections.Generic;

public static class StringUtils
{
    // Static list shared by the whole application
    public static List<string> CallLog { get; } = new List<string>();

    public static bool IsPalindrome(string s)
    {
        CallLog.Add($"IsPalindrome(\"{s}\")");

        return s == Reverse(s);
    }

    public static string Reverse(string s)
    {
        CallLog.Add($"Reverse(\"{s}\")");

        char[] characters = s.ToCharArray();

        Array.Reverse(characters);

        return new string(characters);
    }

    public static int WordCount(string s)
    {
        CallLog.Add($"WordCount(\"{s}\")");

        if (string.IsNullOrWhiteSpace(s))
        {
            return 0;
        }

        return s.Split(
            new[] { ' ', '\t', '\n' },
            StringSplitOptions.RemoveEmptyEntries
        ).Length;
    }
}

public class TrackedWidget
{
    public Guid InstanceId { get; }

    public static int LiveCount { get; private set; }

    public TrackedWidget()
    {
        InstanceId = Guid.NewGuid();
        LiveCount++;
    }

    public void Dispose()
    {
        LiveCount--;
    }

    public void PrintInfo()
    {
        Console.WriteLine(
            $"Widget {InstanceId}: LiveCount={LiveCount}"
        );
    }
}

class Program
{
    static void Main()
    {
        // --------------------------------
        // StringUtils
        // --------------------------------

        Console.WriteLine(
            $"IsPalindrome(\"racecar\") -> " +
            $"{StringUtils.IsPalindrome("racecar")}"
        );

        Console.WriteLine(
            $"Reverse(\"Hello\") -> " +
            $"{StringUtils.Reverse("Hello")}"
        );

        Console.WriteLine(
            $"WordCount(\"the quick brown fox\") -> " +
            $"{StringUtils.WordCount("the quick brown fox")}"
        );

        // This would NOT compile because StringUtils
        // is a static class:
        //
        // StringUtils utils = new StringUtils();


        // --------------------------------
        // TrackedWidget
        // --------------------------------

        TrackedWidget widget1 = new TrackedWidget();
        TrackedWidget widget2 = new TrackedWidget();
        TrackedWidget widget3 = new TrackedWidget();

        Console.WriteLine(
            $"LiveCount after creating 3 widgets: " +
            $"{TrackedWidget.LiveCount}"
        );

        widget1.PrintInfo();
        widget2.PrintInfo();
        widget3.PrintInfo();

        // Dispose two widgets
        widget1.Dispose();
        widget2.Dispose();

        Console.WriteLine(
            $"LiveCount after disposing 2: " +
            $"{TrackedWidget.LiveCount}"
        );


        // --------------------------------
        // Bonus Challenge
        // --------------------------------

        Console.WriteLine("\nMethod call log:");

        foreach (string log in StringUtils.CallLog)
        {
            Console.WriteLine(log);
        }
    }
}