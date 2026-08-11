using System;

class Program
{
    static void Main()
    {

        string original = "  Hello, Training Team!  ";

        string trimmed = original.Trim();

        bool sameObject = object.ReferenceEquals(original, trimmed);

        Console.WriteLine(
            "ReferenceEquals(original, trimmed): " + sameObject);

        bool containsTraining = trimmed.Contains("Training");

        Console.WriteLine(
            "Contains \"Training\": " + containsTraining);

        bool startsWithHello = trimmed.StartsWith("Hello");

        Console.WriteLine(
            "StartsWith trimmed \"Hello\": " + startsWithHello);

        int commaIndex = trimmed.IndexOf(',');

        Console.WriteLine(
            "Index of first comma: " + commaIndex);

        string replaced = trimmed.Replace(
            "Training Team",
            "Engineering Team");

        Console.WriteLine(
            "\"Training Team\" replaced -> " + replaced);

        string[] words = trimmed.Split(
            new char[] { ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        Console.WriteLine(
            "IsNullOrWhiteSpace(null): " +
            string.IsNullOrWhiteSpace(null));

        Console.WriteLine(
            "IsNullOrWhiteSpace(\"\"): " +
            string.IsNullOrWhiteSpace(""));

        Console.WriteLine(
            "IsNullOrWhiteSpace(\"   \"): " +
            string.IsNullOrWhiteSpace("   "));

        Console.WriteLine(
            "IsNullOrWhiteSpace(\"ok\"): " +
            string.IsNullOrWhiteSpace("ok"));

        Console.ReadLine();
    }
}