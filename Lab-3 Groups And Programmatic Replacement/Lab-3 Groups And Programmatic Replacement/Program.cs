using System;
using System.Text.RegularExpressions;
using System.Globalization;

class Lab3
{
    static void Main()
    {
        // TODO 1: Named groups for date/time/level/message

        string logLine = "2026-08-14 09:15:32 ERROR Connection timed out";

        string logPattern =
            @"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2})\s+" +
            @"(?<level>\w+)\s+" +
            @"(?<message>.+)$";

        Match logMatch = Regex.Match(logLine, logPattern);

        Console.WriteLine("TODO 1: Log Information");

        Console.WriteLine("Date: " + logMatch.Groups["date"].Value);
        Console.WriteLine("Time: " + logMatch.Groups["time"].Value);
        Console.WriteLine("Level: " + logMatch.Groups["level"].Value);
        Console.WriteLine("Message: " + logMatch.Groups["message"].Value);


        // TODO 2: Named groups for key/value pairs

        string kvText = "name=Alice;age=30;city=NYC";

        string kvPattern = @"(?<key>[^=;]+)=(?<value>[^;]+)";

        MatchCollection pairs = Regex.Matches(kvText, kvPattern);

        Console.WriteLine("\nTODO 2: Key/Value Pairs");

        foreach (Match pair in pairs)
        {
            Console.WriteLine(
                "Key: " + pair.Groups["key"].Value +
                ", Value: " + pair.Groups["value"].Value
            );
        }


        // TODO 3: MatchEvaluator - format numbers with thousands separators

        string numbers = "Revenue: 1234567, Costs: 89000";

        string formattedNumbers = Regex.Replace(
            numbers,
            @"\b\d+\b",
            match =>
            {
                long number = long.Parse(match.Value);
                return number.ToString("N0", CultureInfo.InvariantCulture);
            }
        );

        Console.WriteLine("\nTODO 3: Formatted Numbers");
        Console.WriteLine(formattedNumbers);


        // TODO 4: MatchEvaluator - convert ALL CAPS words to Title Case

        string shouting = "THIS IS URGENT please respond";

        string convertedShouting = Regex.Replace(
            shouting,
            @"\b[A-Z]{2,}\b",
            match =>
            {
                string word = match.Value.ToLower();
                return char.ToUpper(word[0]) + word.Substring(1);
            }
        );

        Console.WriteLine("\nTODO 4: Converted Sentence");
        Console.WriteLine(convertedShouting);
    }
}