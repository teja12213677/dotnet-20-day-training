using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Designation { get; set; }
    public string Department { get; set; }
    public int ManagerId { get; set; }

    public Employee(int id, string name, string designation, string department, int managerId)
    {
        EmployeeId = id;
        Name = name;
        Designation = designation;
        Department = department;
        ManagerId = managerId;
    }
}

class Program
{
    static List<Employee> employees = new List<Employee>
    {
        new Employee(1001, "John Smith", "CEO", "Management", 0),
        new Employee(1002, "Michael Johnson", "IT Manager", "IT", 1001),
        new Employee(1003, "Sarah Williams", "HR Manager", "HR", 1001),
        new Employee(1004, "David Brown", "Finance Manager", "Finance", 1001),
        new Employee(1005, "Robert Davis", "Team Lead", "IT", 1002),
        new Employee(1006, "Jennifer Miller", "QA Lead", "IT", 1002),
        new Employee(1007, "William Wilson", "Senior Developer", "IT", 1005),
        new Employee(1008, "Emma Moore", "Senior Developer", "IT", 1005),
        new Employee(1009, "Daniel Taylor", "QA Engineer", "IT", 1006),
        new Employee(1010, "Sophia Anderson", "QA Engineer", "IT", 1006),
        new Employee(1011, "James Thomas", "Recruiter", "HR", 1003),
        new Employee(1012, "Olivia Jackson", "Recruiter", "HR", 1003),
        new Employee(1013, "Benjamin White", "Accountant", "Finance", 1004),
        new Employee(1014, "Charlotte Harris", "Accountant", "Finance", 1004),
        new Employee(1015, "Lucas Martin", "Developer", "IT", 1007),
        new Employee(1016, "Ethan Walker", "Developer", "IT", 1007),
        new Employee(1017, "Mia Hall", "UI Developer", "IT", 1008),
        new Employee(1018, "Alexander Young", "Business Analyst", "IT", 1005),
        new Employee(1019, "Harper King", "HR Executive", "HR", 1011),
        new Employee(1020, "Jack Scott", "Finance Executive", "Finance", 1013)
    };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("ABC TECHNOLOGIES");
            Console.WriteLine("Organization Hierarchy Management System");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Display Complete Organization Chart");
            Console.WriteLine("2. Find Employee by ID");
            Console.WriteLine("3. Find Employee by Name");
            Console.WriteLine("4. Display Employees under a Manager");
            Console.WriteLine("5. Count Total Employees under a Manager");
            Console.WriteLine("6. Display Hierarchy Level");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your Choice : ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nOrganization Hierarchy\n");
                    Employee ceo = employees.First(e => e.ManagerId == 0);
                    DisplayHierarchy(ceo.EmployeeId, "", true);
                    break;

                case 2:
                    Console.Write("Enter Employee ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    FindById(id);
                    break;

                case 3:
                    Console.Write("Enter Employee Name: ");
                    string name = Console.ReadLine();
                    FindByName(name);
                    break;

                case 4:
                    Console.Write("Enter Manager ID: ");
                    int managerId = Convert.ToInt32(Console.ReadLine());
                    DisplayEmployees(managerId);
                    break;

                case 5:
                    Console.Write("Enter Manager ID: ");
                    int mgr = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Total Employees: " + CountEmployees(mgr));
                    break;

                case 6:
                    Console.Write("Enter Employee ID: ");
                    int empId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Hierarchy Level: " + GetLevel(empId));
                    break;

                case 7:
                    return;

                default:
                    Console.WriteLine("Invalid Choice!");
                    break;
            }
        }
    }

    // Recursive Organization Chart
    static void DisplayHierarchy(int managerId, string indent, bool isLast)
    {
        Employee emp = employees.FirstOrDefault(e => e.EmployeeId == managerId);

        if (emp == null)
            return;

        if (indent == "")
            Console.WriteLine($"{emp.Name} ({emp.Designation})");
        else
            Console.WriteLine(indent + (isLast ? "└── " : "├── ") + $"{emp.Name} ({emp.Designation})");

        indent += (isLast ? "    " : "│   ");

        List<Employee> subordinates = employees.Where(e => e.ManagerId == managerId).ToList();

        for (int i = 0; i < subordinates.Count; i++)
        {
            DisplayHierarchy(subordinates[i].EmployeeId, indent, i == subordinates.Count - 1);
        }
    }

    // Find by ID
    static void FindById(int id)
    {
        Employee emp = employees.FirstOrDefault(e => e.EmployeeId == id);

        if (emp != null)
        {
            Console.WriteLine("\nEmployee Found");
            Console.WriteLine("ID: " + emp.EmployeeId);
            Console.WriteLine("Name: " + emp.Name);
            Console.WriteLine("Designation: " + emp.Designation);
            Console.WriteLine("Department: " + emp.Department);
        }
        else
        {
            Console.WriteLine("Employee Not Found.");
        }
    }

    // Find by Name
    static void FindByName(string name)
    {
        Employee emp = employees.FirstOrDefault(e =>
            e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (emp != null)
        {
            Console.WriteLine("\nEmployee Found");
            Console.WriteLine("ID: " + emp.EmployeeId);
            Console.WriteLine("Designation: " + emp.Designation);
            Console.WriteLine("Department: " + emp.Department);
        }
        else
        {
            Console.WriteLine("Employee Not Found.");
        }
    }

    // Display Direct Employees
    static void DisplayEmployees(int managerId)
    {
        List<Employee> subs = employees.Where(e => e.ManagerId == managerId).ToList();

        if (subs.Count == 0)
        {
            Console.WriteLine("No Employees Found.");
            return;
        }

        Console.WriteLine("\nEmployees under Manager:");
        foreach (Employee e in subs)
        {
            Console.WriteLine($"{e.EmployeeId} - {e.Name} ({e.Designation})");
        }
    }

    // Recursive Count
    static int CountEmployees(int managerId)
    {
        int count = 0;

        List<Employee> subs = employees.Where(e => e.ManagerId == managerId).ToList();

        foreach (Employee e in subs)
        {
            count++;
            count += CountEmployees(e.EmployeeId);
        }

        return count;
    }

    // Hierarchy Level
    static int GetLevel(int empId)
    {
        Employee emp = employees.FirstOrDefault(e => e.EmployeeId == empId);

        if (emp == null)
            return -1;

        if (emp.ManagerId == 0)
            return 1;

        return 1 + GetLevel(emp.ManagerId);
    }
}
