using System;

class Program
{
    static void Main()
    {
        string[] orders =
        {
            "O101|John|Laptop|55000",
            "O102|Alice|Mobile|25000",
            "O103|David|Headphones|3000",
            "O104|Emma|Keyboard|1500",
            "O105|James|Mouse|800"
        };
        Console.WriteLine("Order Details:");
        foreach (string order in orders)
        {
            string[] data = order.Split('|');
            Console.WriteLine($"Order ID: {data[0]}");
            Console.WriteLine($"Customer: {data[1]}");
            Console.WriteLine($"Product: {data[2]}");
            Console.WriteLine($"Price: ₹{data[3]}");
            Console.WriteLine();
        }
        int maxPrice = 0;
        string customer = "";

        foreach (string order in orders)
        {
            string[] data = order.Split('|');
            int price = int.Parse(data[3]);

            if (price > maxPrice)
            {
                maxPrice = price;
                customer = data[1];
            }
        }

        Console.WriteLine("Highest Price Order:");
        Console.WriteLine(customer + " - ₹" + maxPrice);
        Console.Write("\nEnter Customer Name: ");
        string search = Console.ReadLine();

        foreach (string order in orders)
        {
            string[] data = order.Split('|');

            if (data[1].Equals(search, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Order Found:");
                Console.WriteLine(order);
            }
        }
        Console.WriteLine("\nTotal Orders: " + orders.Length);


        Console.WriteLine("Orders Above ₹10000:");

        foreach (string order in orders)
        {
            string[] data = order.Split('|');
            int price = int.Parse(data[3]);

            if (price > 10000)
            {
                Console.WriteLine($"{data[0]} - {data[1]} - {data[2]} - ₹{price}");
            }
        }

        int totalRevenue = 0;

        foreach (string order in orders)
        {
            string[] data = order.Split('|');
            totalRevenue += int.Parse(data[3]);
        }

        Console.WriteLine("Total Revenue = ₹" + totalRevenue);
    }
}