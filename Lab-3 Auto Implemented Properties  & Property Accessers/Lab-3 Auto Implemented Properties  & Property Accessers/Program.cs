using System;

public class Subscription
{
    // Get-only auto-property
    // Can only be assigned in the constructor
    public string Id { get; }

    // Fully accessible get/set property
    public string PlanName { get; set; } = string.Empty;

    // Init-only property
    // Can be assigned during object initialization,
    // but not after construction
    public DateTime StartedAt { get; init; }

    // Public getter, private setter
    public bool IsActive { get; private set; } = true;

    // Computed expression-bodied property
    public int MonthsActive =>
        (DateTime.Now.Year - StartedAt.Year) * 12
        + DateTime.Now.Month - StartedAt.Month;

    // Constructor
    public Subscription(string id)
    {
        Id = id;
    }

    // Method can use the private setter
    public void Cancel()
    {
        IsActive = false;
    }

    // Bonus method
    public void Renew(string newPlanName)
    {
        PlanName = newPlanName;
        IsActive = true;
    }
}

class Program
{
    static void Main()
    {
        // Create Subscription
        Subscription subscription = new Subscription("SUB-1")
        {
            PlanName = "Pro",
            StartedAt = new DateTime(2026, 1, 1)
        };

        // Print all properties
        Console.WriteLine(
            $"Id={subscription.Id}, " +
            $"Plan={subscription.PlanName}, " +
            $"Started={subscription.StartedAt:yyyy-MM-dd}, " +
            $"Active={subscription.IsActive}, " +
            $"MonthsActive={subscription.MonthsActive}"
        );

        // Cancel subscription
        subscription.Cancel();

        Console.WriteLine(
            $"After Cancel(): Active={subscription.IsActive}"
        );

        // These statements would NOT compile:

        // subscription.IsActive = true;
        // Error: The property or indexer 'Subscription.IsActive'
        // cannot be used in this context because the set accessor
        // is inaccessible.

        // subscription.StartedAt = DateTime.Now;
        // Error: The property or indexer 'Subscription.StartedAt'
        // cannot be assigned to -- it is read only.


        // Bonus Challenge
        subscription.Renew("Premium");

        Console.WriteLine(
            $"After Renew(): Plan={subscription.PlanName}, " +
            $"Active={subscription.IsActive}"
        );
    }
}