using System;
using System.Collections.Generic;
using System.Linq;

class Ticket
{
    public string TicketId { get; set; }
    public string CustomerName { get; set; }
    public string IssueType { get; set; }

    public Ticket(string ticketId, string customerName, string issueType)
    {
        TicketId = ticketId;
        CustomerName = customerName;
        IssueType = issueType;
    }

    public override string ToString()
    {
        return $"{TicketId} | {CustomerName} | {IssueType}";
    }
}

class Program
{
    static void Main()
    {
        Queue<Ticket> tickets = new Queue<Ticket>();

        while (true)
        {
            Console.WriteLine("\n=====================================");
            Console.WriteLine(" Customer Support Ticket Management ");
            Console.WriteLine("=====================================");
            Console.WriteLine("1. Enqueue Ticket");
            Console.WriteLine("2. Display All Tickets");
            Console.WriteLine("3. Process First Ticket");
            Console.WriteLine("4. View Next Ticket");
            Console.WriteLine("5. Check Queue Count");
            Console.WriteLine("6. Search Ticket by ID");
            Console.WriteLine("7. Count Tickets by Issue Type");
            Console.WriteLine("8. Remove All Processed Tickets");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Ticket ID: ");
                    string id = Console.ReadLine();

                    Console.Write("Enter Customer Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Issue Type: ");
                    string issue = Console.ReadLine();

                    tickets.Enqueue(new Ticket(id, name, issue));
                    Console.WriteLine("Ticket Added Successfully.");
                    break;

                case 2:
                    if (tickets.Count == 0)
                    {
                        Console.WriteLine("No tickets available.");
                    }
                    else
                    {
                        Console.WriteLine("\nCurrent Tickets:");
                        foreach (Ticket t in tickets)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    break;

                case 3:
                    if (tickets.Count == 0)
                    {
                        Console.WriteLine("No tickets to process.");
                    }
                    else
                    {
                        Ticket processed = tickets.Dequeue();
                        Console.WriteLine("Processed Ticket:");
                        Console.WriteLine(processed);
                    }
                    break;

                case 4:
                    if (tickets.Count == 0)
                    {
                        Console.WriteLine("Queue is empty.");
                    }
                    else
                    {
                        Console.WriteLine("Next Ticket:");
                        Console.WriteLine(tickets.Peek());
                    }
                    break;

                case 5:
                    Console.WriteLine("Total Tickets in Queue: " + tickets.Count);
                    break;

                case 6:
                    Console.Write("Enter Ticket ID to search: ");
                    string searchId = Console.ReadLine();

                    Ticket foundTicket = tickets.FirstOrDefault(t => t.TicketId == searchId);

                    if (foundTicket != null)
                        Console.WriteLine("Found: " + foundTicket);
                    else
                        Console.WriteLine("Ticket not found.");
                    break;

                case 7:
                    if (tickets.Count == 0)
                    {
                        Console.WriteLine("No tickets available.");
                    }
                    else
                    {
                        var groups = tickets.GroupBy(t => t.IssueType);

                        Console.WriteLine("Tickets by Issue Type:");

                        foreach (var group in groups)
                        {
                            Console.WriteLine($"{group.Key} : {group.Count()}");
                        }
                    }
                    break;

                case 8:
                    tickets.Clear();
                    Console.WriteLine("All processed tickets removed. Queue is now empty.");
                    break;

                case 9:
                    Console.WriteLine("Thank You!");
                    return;

                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }
    }
}