using System;
using System.Text.RegularExpressions;

public static class PatternLibrary
{
    // TODO 1: Static readonly Regex fields using RegexOptions.Compiled

    public static readonly Regex Email = new Regex(
        @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
        RegexOptions.Compiled
    );

    public static readonly Regex UsPhone = new Regex(
        @"^\d{3}-\d{3}-\d{4}$",
        RegexOptions.Compiled
    );

    public static readonly Regex HexColor = new Regex(
        @"^#[0-9A-Fa-f]{6}$",
        RegexOptions.Compiled
    );


    // TODO 2: Wrapper methods

    public static bool IsValidEmail(string input)
    {
        return Email.IsMatch(input);
    }

    public static bool IsValidPhone(string input)
    {
        return UsPhone.IsMatch(input);
    }

    public static bool IsValidHexColor(string input)
    {
        return HexColor.IsMatch(input);
    }
}


class Lab4
{
    static void Main()
    {
        // TODO 3: RegexOptions.IgnoreCase demo

        string pattern = "hello";

        Console.WriteLine("TODO 3: IgnoreCase Demo");

        Console.WriteLine(
            "Without IgnoreCase - HELLO: " +
            Regex.IsMatch("HELLO", pattern)
        );

        Console.WriteLine(
            "Without IgnoreCase - hello: " +
            Regex.IsMatch("hello", pattern)
        );

        Console.WriteLine(
            "With IgnoreCase - HELLO: " +
            Regex.IsMatch("HELLO", pattern, RegexOptions.IgnoreCase)
        );

        Console.WriteLine(
            "With IgnoreCase - hello: " +
            Regex.IsMatch("hello", pattern, RegexOptions.IgnoreCase)
        );


        // TODO 4: RegexOptions.Multiline demo

        string multiLineText =
            "First line\nSecond line\nThird line";

        // Without Multiline
        MatchCollection withoutMultiline = Regex.Matches(
            multiLineText,
            @"^"
        );

        // With Multiline
        MatchCollection withMultiline = Regex.Matches(
            multiLineText,
            @"^",
            RegexOptions.Multiline
        );

        Console.WriteLine("\nTODO 4: Multiline Demo");

        Console.WriteLine(
            "Without Multiline: " +
            withoutMultiline.Count
        );

        Console.WriteLine(
            "With Multiline: " +
            withMultiline.Count
        );


        // TODO 5: Test PatternLibrary

        Console.WriteLine("\nTODO 5: PatternLibrary Tests");

        // Email
        Console.WriteLine(
            "Valid Email: " +
            PatternLibrary.IsValidEmail("user@example.com")
        );

        Console.WriteLine(
            "Invalid Email: " +
            PatternLibrary.IsValidEmail("user@")
        );

        // US Phone
        Console.WriteLine(
            "Valid Phone: " +
            PatternLibrary.IsValidPhone("123-456-7890")
        );

        Console.WriteLine(
            "Invalid Phone: " +
            PatternLibrary.IsValidPhone("1234567890")
        );

        // Hex Color
        Console.WriteLine(
            "Valid Hex Color: " +
            PatternLibrary.IsValidHexColor("#1A2B3C")
        );

        Console.WriteLine(
            "Invalid Hex Color: " +
            PatternLibrary.IsValidHexColor("#GGGGGG")
        );
    }
}