using System;
using System.Text.RegularExpressions;

class Lab2
{
    static void Main()
    {
        // TODO 1: Matches + IgnoreCase - print each order number

        string text = "Order #4521 was shipped. order #99 is pending. ORDER #12345 was cancelled.";

        // Capture only the numeric part after #
        MatchCollection orders = Regex.Matches(
            text,
            @"Order\s+#(\d+)",
            RegexOptions.IgnoreCase
        );

        Console.WriteLine("TODO 1: Order Numbers");

        foreach (Match order in orders)
        {
            Console.WriteLine(order.Groups[1].Value);
        }


        // TODO 2: Replace to mask all but the last 4 digits

        string cardText = "Card on file: 4111-1111-1111-1234";

        // Capture the first 12 digits/groups and keep the last 4 digits.
        string maskedCard = Regex.Replace(
            cardText,
            @"\d{4}[- ]\d{4}[- ]\d{4}[- ](\d{4})",
            "XXXX-XXXX-XXXX-$1"
        );

        Console.WriteLine("\nTODO 2: Masked Card");
        Console.WriteLine(maskedCard);


        // TODO 3: Replace with capturing groups -> "John Smith"

        string names = "Smith, John";

        string formattedName = Regex.Replace(
            names,
            @"^([^,]+),\s*(.+)$",
            "$2 $1"
        );

        Console.WriteLine("\nTODO 3: Formatted Name");
        Console.WriteLine(formattedName);


        // TODO 4: Split into a clean array of trimmed tags

        string tags = "red, blue;green , yellow";

        // Split using either comma or semicolon.
        string[] tagArray = Regex.Split(tags, @"[,;]");

        Console.WriteLine("\nTODO 4: Clean Tags");

        foreach (string tag in tagArray)
        {
            Console.WriteLine(tag.Trim());
        }
    }
}