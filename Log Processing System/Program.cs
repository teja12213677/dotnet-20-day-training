using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string LogLevel { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }

    public LogEntry(DateTime timestamp, string logLevel, string message, string exception = "")
    {
        Timestamp = timestamp;
        LogLevel = logLevel;
        Message = message;
        Exception = exception;
    }
}

class LogProcessor
{
    private StringBuilder buffer;
    private List<string> errorLogs;

    private int bufferCapacity;
    private string filePath;

    public LogProcessor(int bufferCapacity, string filePath)
    {
        this.bufferCapacity = bufferCapacity;
        this.filePath = filePath;

        buffer = new StringBuilder();
        errorLogs = new List<string>();
    }

    public void ProcessLog(LogEntry log)
    {
        // StringBuilder is used to efficiently construct the log message
        StringBuilder logBuilder = new StringBuilder();

        logBuilder.Append("[");
        logBuilder.Append(log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
        logBuilder.Append("] ");

        logBuilder.Append(log.LogLevel);
        logBuilder.Append(": ");
        logBuilder.Append(log.Message);

        if (!string.IsNullOrEmpty(log.Exception))
        {
            logBuilder.Append(" | Exception: ");
            logBuilder.Append(log.Exception);
        }

        logBuilder.AppendLine();

        // Add formatted log to buffer
        buffer.Append(logBuilder.ToString());

        // Store ERROR logs separately
        if (log.LogLevel.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
        {
            errorLogs.Add(logBuilder.ToString());
        }

        // Flush when buffer reaches configured capacity
        if (buffer.Length >= bufferCapacity)
        {
            FlushBuffer();
        }
    }

    private void FlushBuffer()
    {
        if (buffer.Length == 0)
            return;

        File.AppendAllText(filePath, buffer.ToString());

        Console.WriteLine("Buffer flushed to file.");

        buffer.Clear();
    }

    public void Flush()
    {
        FlushBuffer();
    }

    public void DisplayErrorSummary()
    {
        Console.WriteLine("\n===== ERROR SUMMARY =====");

        Console.WriteLine("Total Errors: " + errorLogs.Count);

        foreach (string error in errorLogs)
        {
            Console.Write(error);
        }
    }
}

class Program
{
    static void Main()
    {
        LogProcessor processor = new LogProcessor(
            200,
            "logs.txt"
        );

        // Create log entries
        LogEntry log1 = new LogEntry(
            DateTime.Now,
            "INFO",
            "Application started"
        );

        LogEntry log2 = new LogEntry(
            DateTime.Now,
            "INFO",
            "User logged in"
        );

        LogEntry log3 = new LogEntry(
            DateTime.Now,
            "ERROR",
            "Database connection failed",
            "SqlException: Connection timeout"
        );

        LogEntry log4 = new LogEntry(
            DateTime.Now,
            "WARNING",
            "Memory usage is high"
        );

        LogEntry log5 = new LogEntry(
            DateTime.Now,
            "ERROR",
            "File could not be found",
            "FileNotFoundException"
        );

        // Process logs
        processor.ProcessLog(log1);
        processor.ProcessLog(log2);
        processor.ProcessLog(log3);
        processor.ProcessLog(log4);
        processor.ProcessLog(log5);

        // Flush remaining logs
        processor.Flush();

        // Display error summary
        processor.DisplayErrorSummary();

        Console.WriteLine("\nLogs have been written to logs.txt");

        Console.ReadLine();
    }
}