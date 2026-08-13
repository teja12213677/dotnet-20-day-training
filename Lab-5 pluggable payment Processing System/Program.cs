using System;
using System.Collections.Generic;
using System.Linq;

// 1. Basic interface
public interface IIdentifiable
{
    string Id { get; }
}

// 2. Payment interface inherits from IIdentifiable
public interface IPaymentMethod : IIdentifiable
{
    string DisplayName { get; }

    PaymentResult Charge(decimal amount);
}

// 3. Encapsulated PaymentResult class
public class PaymentResult
{
    public bool Success { get; }
    public string Message { get; }

    public PaymentResult(bool success, string message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        Success = success;
        Message = message;
    }
}

// 4. Abstract base class
public abstract class PaymentMethodBase : IPaymentMethod
{
    public string Id { get; }
    public string DisplayName { get; }

    protected PaymentMethodBase(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public abstract PaymentResult Charge(decimal amount);
}

// 5. Credit Card implementation
public class CreditCardPayment : PaymentMethodBase
{
    public CreditCardPayment(string id, string displayName)
        : base(id, displayName)
    {
    }

    public override PaymentResult Charge(decimal amount)
    {
        if (amount > 5000)
        {
            return new PaymentResult(
                false,
                "Credit card limit exceeded");
        }

        return new PaymentResult(
            true,
            "Credit card payment successful");
    }
}

// 6. Cash implementation
public sealed class CashPayment : PaymentMethodBase
{
    public CashPayment(string id, string displayName)
        : base(id, displayName)
    {
    }

    public override PaymentResult Charge(decimal amount)
    {
        return new PaymentResult(
            true,
            "Cash payment successful");
    }
}

class Program
{
    static void Main()
    {
        // 7. Create different payment methods
        List<IPaymentMethod> paymentMethods =
            new List<IPaymentMethod>
            {
                new CreditCardPayment("CC-1", "Visa ...1234"),
                new CashPayment("CASH-1", "Cash Drawer")
            };

        // Amounts to charge
        decimal[] amounts = { 1500m, 6000m };

        // 8. Anonymous-type settlement report
        var settlementReport =
            from payment in paymentMethods
            from amount in amounts
            let result = payment.Charge(amount)
            select new
            {
                Id = payment.Id,
                DisplayName = payment.DisplayName,
                AmountAttempted = amount,
                Success = result.Success
            };

        // 9. Print settlement report
        foreach (var entry in settlementReport)
        {
            Console.WriteLine(
                $"{entry.Id}  {entry.DisplayName}  " +
                $"Attempted={entry.AmountAttempted:F2}  " +
                $"Success={entry.Success}");
        }

        Console.WriteLine();

        // 10. Calculate successfully settled amount
        decimal totalSettled = settlementReport
            .Where(x => x.Success)
            .Sum(x => x.AmountAttempted);

        Console.WriteLine(
            $"Total successfully settled: {totalSettled:F2}");
    }
}