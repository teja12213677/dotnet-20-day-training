using System;
using System.Globalization;
using System.Text;

static class StringToolkit
{

    public static string Reverse(string input)
    {
        char[] characters = input.ToCharArray();

        Array.Reverse(characters);

        return new string(characters);
    }

    public static int CountChar(string text, char searchChar)
    {
        int count = 0;

        foreach (char character in text)
        {
            if (character == searchChar)
            {
                count++;
            }
        }

        return count;
    }


    public static string RemoveDuplicates(string input)
    {
        StringBuilder result = new StringBuilder();

        foreach (char character in input)
        {
            if (!result.ToString().Contains(character))
            {
                result.Append(character);
            }
        }

        return result.ToString();
    }
    public static bool IsPalindrome(string input)
    {
        string cleaned = input.Replace(" ", "").ToLower();

        string reversed = Reverse(cleaned);

        return cleaned == reversed;
    }

    public static string ToTitleCase(string input)
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        return textInfo.ToTitleCase(input.ToLower());
    }

    public static string ExtractNumbers(string input)
    {
        StringBuilder numbers = new StringBuilder();

        foreach (char character in input)
        {
            if (char.IsDigit(character))
            {
                numbers.Append(character);
            }
        }

        return numbers.ToString();
    }
}

class Program
{
    static void Main()
    {

        string reversed = StringToolkit.Reverse("Hello");

        Console.WriteLine(
            "Reverse(\"Hello\") -> \"" + reversed + "\"");

        int count = StringToolkit.CountChar("banana", 'a');

        Console.WriteLine(
            "CountChar(\"banana\", 'a') -> " + count);


        string withoutDuplicates =
            StringToolkit.RemoveDuplicates("mississippi");

        Console.WriteLine(
            "RemoveDuplicates(\"mississippi\") -> \""
            + withoutDuplicates + "\"");

        bool palindrome =
            StringToolkit.IsPalindrome("race car");

        Console.WriteLine(
            "IsPalindrome(\"race car\") -> " + palindrome);

        string title =
            StringToolkit.ToTitleCase("hello training team");

        Console.WriteLine(
            "ToTitleCase(\"hello training team\") -> \""
            + title + "\"");

        string numbers =
            StringToolkit.ExtractNumbers("Order #4521, qty 3");

        Console.WriteLine(
            "ExtractNumbers(\"Order #4521, qty 3\") -> \""
            + numbers + "\"");


        Console.ReadLine();
    }
}