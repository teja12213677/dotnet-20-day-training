using System;
using System.Collections.Generic;
using System.Linq;

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

public class Order
{
    public string OrderId { get; }

    public Address? ShipTo { get; set; }

    public List<string> Items { get; set; } = new();

    public decimal Total { get; set; }

    public Order(string orderId)
    {
        OrderId = orderId;
    }
}

class Program
{
    static void Main()
    {
        // Order with nested Address initializer
        // and collection initializer
        Order order1 = new Order("ORD-1")
        {
            ShipTo = new Address
            {
                Street = "123 Main Street",
                City = "Springfield",
                ZipCode = "62701"
            },

            Items =
            {
                "Laptop",
                "Mouse"
            },

            Total = 59.98m
        };

        // Print Order 1
        Console.WriteLine(
            $"Order {order1.OrderId} ships to {order1.ShipTo?.City} " +
            $"with {order1.Items.Count} items, Total=${order1.Total:F2}"
        );


        // Second order with ShipTo left as null
        Order order2 = new Order("ORD-2")
        {
            Items =
            {
                "Keyboard",
                "Headset"
            },

            Total = 100.00m
        };

        // Check for null ShipTo
        if (order2.ShipTo == null)
        {
            Console.WriteLine(
                $"Order {order2.OrderId} has no shipping address set " +
                $"(ShipTo is null)"
            );
        }


        // --------------------------------
        // Bonus Challenge
        // --------------------------------

        List<Order> orders = new List<Order>
        {
            new Order("ORD-3")
            {
                ShipTo = new Address
                {
                    Street = "10 Park Road",
                    City = "Dallas",
                    ZipCode = "75001"
                },
                Items =
                {
                    "Monitor",
                    "Keyboard"
                },
                Total = 200.00m
            },

            new Order("ORD-4")
            {
                Items =
                {
                    "Webcam"
                },
                Total = 75.50m
            }
        };

        decimal totalOrders = orders.Sum(order => order.Total);

        Console.WriteLine(
            $"Total of bonus orders: ${totalOrders:F2}"
        );
    }
}