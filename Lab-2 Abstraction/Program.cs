using System;
using System.Collections.Generic;
using System.Linq;

public abstract class NotificationChannel
{
    // Concrete method
    public bool TrySend(string message)
    {
        try
        {
            return Send(message);
        }
        catch
        {
            return false;
        }
    }

    // Abstract method
    protected abstract bool Send(string message);
}

// Email implementation
public class EmailChannel : NotificationChannel
{
    protected override bool Send(string message)
    {
        // Email always succeeds
        return true;
    }
}

// SMS implementation
public class SmsChannel : NotificationChannel
{
    protected override bool Send(string message)
    {
        // SMS cannot contain more than 160 characters
        if (message.Length > 160)
        {
            throw new Exception("SMS message is too long");
        }

        return true;
    }
}

class Program
{
    static void Main()
    {
        // Create notification channels
        List<NotificationChannel> channels = new List<NotificationChannel>
        {
            new EmailChannel(),
            new SmsChannel(),
            new EmailChannel(),
            new SmsChannel()
        };

        // Short message
        string shortMessage = "Hello, this is a short message.";

        // Long message - more than 160 characters
        string longMessage =
            "This is a very long message that contains more than 160 characters. " +
            "It is being used to test the SMS channel validation and exception handling. " +
            "The email channel should still succeed.";

        // Store results using anonymous types
        var report = new[]
        {
            new
            {
                ChannelType = channels[0].GetType().Name,
                Success = channels[0].TrySend(shortMessage)
            },

            new
            {
                ChannelType = channels[1].GetType().Name,
                Success = channels[1].TrySend(shortMessage)
            },

            new
            {
                ChannelType = channels[2].GetType().Name,
                Success = channels[2].TrySend(shortMessage)
            },

            new
            {
                ChannelType = channels[3].GetType().Name,
                Success = channels[3].TrySend(longMessage)
            }
        };

        // Print report
        foreach (var entry in report)
        {
            string result = entry.Success ? "Success" : "Failed";

            Console.WriteLine($"{entry.ChannelType}: {result}");
        }

        // Count successful and failed messages
        int succeeded = report.Count(x => x.Success);
        int failed = report.Count(x => !x.Success);

        Console.WriteLine();
        Console.WriteLine($"Succeeded: {succeeded}, Failed: {failed}");
    }
}