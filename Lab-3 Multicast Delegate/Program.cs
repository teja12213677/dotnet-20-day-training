using System;

class Lab3
{
    // 1. Declare the delegate
    public delegate void OrderEvent(string orderId);

    // 2. Three separate handler methods

    public static void LogToConsole(string orderId)
    {
        Console.WriteLine(
            $"[Console Log] Order {orderId} has been logged.");
    }

    public static void SendEmailSimulation(string orderId)
    {
        Console.WriteLine(
            $"[Email] Confirmation email sent for order {orderId}.");
    }

    public static void UpdateInventorySimulation(string orderId)
    {
        Console.WriteLine(
            $"[Inventory] Inventory updated for order {orderId}.");
    }

    static void Main()
    {
        string orderId = "ORD-1001";

        Console.WriteLine("======================================");
        Console.WriteLine(" Lab 3 - Multicast Delegates");
        Console.WriteLine("======================================");


        // ============================================================
        // PART 1-3: Add three methods to a multicast delegate
        // ============================================================

        Console.WriteLine("\n1. Adding Three Handlers");
        Console.WriteLine("-------------------------");

        OrderEvent orderHandler = LogToConsole;

        orderHandler += SendEmailSimulation;
        orderHandler += UpdateInventorySimulation;

        Console.WriteLine("Invoking all three handlers:");

        orderHandler(orderId);


        // ============================================================
        // PART 4: Remove one handler
        // ============================================================

        Console.WriteLine("\n2. Removing Email Handler");
        Console.WriteLine("-------------------------");

        orderHandler -= SendEmailSimulation;

        Console.WriteLine("Invoking after removing SendEmailSimulation:");

        orderHandler(orderId);


        // ============================================================
        // PART 5: Lambda reference-equality pitfall
        // ============================================================

        Console.WriteLine("\n3. Lambda Reference-Equality Pitfall");
        Console.WriteLine("------------------------------------");

        OrderEvent lambdaHandler = null;

        // Two separate lambda instances are created.
        lambdaHandler += id =>
            Console.WriteLine($"[Lambda A] Processing order {id}");

        lambdaHandler += id =>
            Console.WriteLine($"[Lambda A] Processing order {id}");

        Console.WriteLine("\nBoth identical-looking lambdas are subscribed:");

        lambdaHandler(orderId);


        // ------------------------------------------------------------
        // Attempt to remove using a freshly-created lambda
        // ------------------------------------------------------------

        Console.WriteLine("\nTrying to remove one lambda using a new lambda:");

        lambdaHandler -= id =>
            Console.WriteLine($"[Lambda A] Processing order {id}");

        Console.WriteLine(
            "The unsubscribe did NOT remove the original lambda.");

        lambdaHandler(orderId);


        // ============================================================
        // Fix: Store the original lambda reference
        // ============================================================

        Console.WriteLine("\n4. Correct Lambda Removal");
        Console.WriteLine("-------------------------");

        OrderEvent storedLambda = id =>
            Console.WriteLine($"[Stored Lambda] Processing order {id}");

        lambdaHandler += storedLambda;

        Console.WriteLine("\nAfter adding the stored lambda:");

        lambdaHandler(orderId);


        // Remove the exact same delegate instance
        lambdaHandler -= storedLambda;

        Console.WriteLine(
            "\nAfter removing using the stored delegate reference:");

        lambdaHandler(orderId);

        Console.WriteLine("\n======================================");
        Console.WriteLine(" Program completed successfully.");
        Console.WriteLine("======================================");
    }
}