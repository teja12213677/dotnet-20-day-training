using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class LogEntry
{
    public string Date { get; init; } = string.Empty;
    public string Time { get; init; } = string.Empty;
    public string Level { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

class Lab5
{
    // TODO 2: Parse the complete log
    public static List<LogEntry> ParseLog(string rawLog)
    {
        List<LogEntry> entries = new List<LogEntry>();

        // Named groups:
        // date    -> yyyy-MM-dd
        // time    -> HH:mm:ss
        // level   -> INFO, WARN, ERROR
        // message -> remaining text on the line
        string pattern =
            @"^(?<date>\d{4}-\d{2}-\d{2})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2})\s+" +
            @"(?<level>INFO|WARN|ERROR)\s+" +
            @"(?<message>.*)$";

        MatchCollection matches = Regex.Matches(
            rawLog,
            pattern,
            RegexOptions.Multiline
        );

        foreach (Match match in matches)
        {
            LogEntry entry = new LogEntry
            {
                Date = match.Groups["date"].Value,
                Time = match.Groups["time"].Value,
                Level = match.Groups["level"].Value,
                Message = match.Groups["message"].Value
            };

            entries.Add(entry);
        }

        return entries;
    }


    // TODO 4: Redact error codes only from ERROR lines
    public static string RedactErrorCodes(string rawLog)
    {
        // Match a complete ERROR line and capture everything around code=NNN.
        string pattern =
            @"^(?<prefix>\d{4}-\d{2}-\d{2}\s+" +
            @"\d{2}:\d{2}:\d{2}\s+ERROR\b.*?\bcode=)\d{3}(?<suffix>\b.*)$";

        string result = Regex.Replace(
            rawLog,
            pattern,
            match =>
            {
                return match.Groups["prefix"].Value +
                       "###" +
                       match.Groups["suffix"].Value;
            },
            RegexOptions.Multiline
        );

        return result;
    }


    static void Main()
    {
        // TODO 1: Multi-line raw log
        string rawLog =
            "2026-08-14 09:15:32 INFO Application started\n" +
            "2026-08-14 09:16:10 INFO User logged in\n" +
            "2026-08-14 09:17:45 WARN Disk space is getting low\n" +
            "2026-08-14 09:18:22 ERROR Database connection failed code=404\n" +
            "2026-08-14 09:19:05 INFO Request processed successfully\n" +
            "2026-08-14 09:20:30 ERROR Internal server failure code=500";


        // Parse the log
        List<LogEntry> entries = ParseLog(rawLog);


        // TODO 3: LINQ summary
        var summary = entries
            .GroupBy(entry => entry.Level)
            .Select(group => new
            {
                Level = group.Key,
                Count = group.Count()
            });

        Console.WriteLine("LOG SUMMARY");
        Console.WriteLine("-----------");

        foreach (var item in summary)
        {
            Console.WriteLine($"{item.Level}: {item.Count}");
        }


        // Print all parsed entries
        Console.WriteLine("\nPARSED LOG ENTRIES");
        Console.WriteLine("------------------");

        foreach (LogEntry entry in entries)
        {
            Console.WriteLine(
                $"{entry.Date} {entry.Time} [{entry.Level}] {entry.Message}"
            );
        }


        // TODO 4: Redact error codes
        string redactedLog = RedactErrorCodes(rawLog);

        Console.WriteLine("\nREDACTED LOG");
        Console.WriteLine("------------");
        Console.WriteLine(redactedLog);
    }
}