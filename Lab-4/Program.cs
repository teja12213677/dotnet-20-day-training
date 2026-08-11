using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

static class StringToolkit
{

    public static string ToTitleCase(string input)
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        return textInfo.ToTitleCase(input.ToLower());
    }
}

class Program
{
    static void Main()
    {
        const string rawData = @"
john smith|engineering|72000
MARY jones|sales|65000

ravi KUMAR|engineering|81000
";

        List<Employee> employees = new List<Employee>();

        string[] rows = rawData.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries);

        foreach (string row in rows)
        {
     
            if (string.IsNullOrWhiteSpace(row))
            {
                continue;
            }

            string[] fields = row.Split('|');

            string name = fields[0];
            string department = fields[1];
            decimal salary = decimal.Parse(fields[2]);

            Employee employee = new Employee
            {
                Name = name,
                Department = department,
                Salary = salary
            };

            employees.Add(employee);
        }

        StringBuilder sb = new StringBuilder();

        int appendCount = 0;

        sb.AppendLine("==================================================");
        appendCount++;

        sb.AppendLine("        EMPLOYEE COMPENSATION REPORT");
        appendCount++;

        sb.AppendLine("==================================================");
        appendCount++;

        sb.AppendLine(
            "Name".PadRight(20) +
            "Department".PadRight(20) +
            "Salary".PadLeft(10));
        appendCount++;

        sb.AppendLine("--------------------------------------------------");
        appendCount++;

        decimal totalSalary = 0;

        foreach (Employee employee in employees)
        {
            string name =
                StringToolkit.ToTitleCase(employee.Name);

            string department =
                StringToolkit.ToTitleCase(employee.Department);

            string salary =
                employee.Salary.ToString("N0");

            sb.AppendLine(
                name.PadRight(20) +
                department.PadRight(20) +
                salary.PadLeft(10));

            appendCount++;

            totalSalary += employee.Salary;
        }

        sb.AppendLine("--------------------------------------------------");
        appendCount++;

        sb.AppendLine(
            "Employees: " + employees.Count +
            " Total Salary: " +
            totalSalary.ToString("N0"));
        appendCount++;

        sb.AppendLine("==================================================");
        appendCount++;

        Console.WriteLine(sb.ToString());

        Console.WriteLine(
            "StringBuilder Append calls: " + appendCount);

        Console.WriteLine(
            "String concatenations in loop: 0");

        Console.ReadLine();
    }
}