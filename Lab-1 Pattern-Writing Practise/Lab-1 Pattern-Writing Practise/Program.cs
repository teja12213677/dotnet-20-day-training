using System;
using System.Text.RegularExpressions;

class Lab1
{
    static void Main()
    {
        // TODO 1: ZIP code pattern
        // Matches either 5 digits or 5+4 digits.
        string zipPattern = @"^\d{5}(-\d{4})?$";

        Console.WriteLine("TODO 1: ZIP Code");
        Console.WriteLine(Regex.IsMatch("12345", zipPattern));       // True
        Console.WriteLine(Regex.IsMatch("12345-6789", zipPattern)); // True
        Console.WriteLine(Regex.IsMatch("1234", zipPattern));       // False


        // TODO 2: Username pattern
        // 3-16 characters, only letters/digits/underscore,
        // and must not start with a digit.
        string usernamePattern = @"^[A-Za-z_][A-Za-z0-9_]{2,15}$";

        Console.WriteLine("\nTODO 2: Username");
        Console.WriteLine(Regex.IsMatch("user_1", usernamePattern)); // True
        Console.WriteLine(Regex.IsMatch("1user", usernamePattern));  // False
        Console.WriteLine(Regex.IsMatch("ab", usernamePattern));     // False


        // TODO 3: Hex color pattern
        // # followed by exactly 6 hexadecimal characters.
        string hexPattern = @"^#[0-9A-Fa-f]{6}$";

        Console.WriteLine("\nTODO 3: Hex Color");
        Console.WriteLine(Regex.IsMatch("#1A2B3C", hexPattern)); // True
        Console.WriteLine(Regex.IsMatch("#GGGGGG", hexPattern)); // False
        Console.WriteLine(Regex.IsMatch("1A2B3C", hexPattern));  // False


        // TODO 4: Password strength check
        // Using multiple checks instead of one giant regex.
        // Requirement: at least 8 characters, one digit,
        // and one uppercase letter.
        string[] passwords = { "password", "Password1", "pass1" };

        Console.WriteLine("\nTODO 4: Password");

        foreach (string password in passwords)
        {
            bool hasMinimumLength = password.Length >= 8;
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasUppercase = Regex.IsMatch(password, @"[A-Z]");

            bool isStrong = hasMinimumLength && hasDigit && hasUppercase;

            Console.WriteLine($"{password}: {isStrong}");
        }


        // TODO 5: Single-terminator sentence pattern
        // Allows letters, spaces, and common sentence characters,
        // but the sentence must end with exactly one . ! or ?.
        string sentencePattern = @"^[A-Za-z ]+[.!?]$";

        Console.WriteLine("\nTODO 5: Sentence");
        Console.WriteLine(Regex.IsMatch("Hello there.", sentencePattern)); // True
        Console.WriteLine(Regex.IsMatch("Wait...", sentencePattern));      // False
        Console.WriteLine(Regex.IsMatch("Really?", sentencePattern));      // True
    }
}