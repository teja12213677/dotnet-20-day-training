using System;

public class TaxCalculator
{
    // Base tax rate = 10%
    public virtual decimal CalculateTax(decimal amount)
    {
        return amount * 0.1m;
    }
}

public class RegionalTaxCalculator : TaxCalculator
{
    // Regional tax rate = 12%
    // sealed prevents further subclasses from overriding this method
    public sealed override decimal CalculateTax(decimal amount)
    {
        return amount * 0.12m;
    }
}

// This class CANNOT override CalculateTax because the method
// in RegionalTaxCalculator is sealed.
//
// If you uncomment this code, the compiler will give an error:
// "cannot override inherited member ... because it is sealed"

/*
public class InvalidTaxCalculator : RegionalTaxCalculator
{
    public override decimal CalculateTax(decimal amount)
    {
        return amount * 0.15m;
    }
}
*/

// Completely sealed class
public sealed class FixedDiscountCalculator
{
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.9m;
    }
}

// This class CANNOT inherit from FixedDiscountCalculator
//
// If you uncomment this code, the compiler will give an error:
// "cannot derive from sealed type FixedDiscountCalculator"

/*
public class InvalidDiscountCalculator : FixedDiscountCalculator
{
}
*/

class Program
{
    static void Main()
    {
        // RegionalTaxCalculator can be used normally
        RegionalTaxCalculator regionalTax = new RegionalTaxCalculator();

        decimal tax = regionalTax.CalculateTax(200);

        Console.WriteLine(
            $"RegionalTaxCalculator.CalculateTax(200) -> {tax:F2}");

        // FixedDiscountCalculator can also be used normally
        FixedDiscountCalculator discount =
            new FixedDiscountCalculator();

        decimal discountedPrice = discount.ApplyDiscount(50);

        Console.WriteLine(
            $"FixedDiscountCalculator.ApplyDiscount(50) -> {discountedPrice:F2}");
    }
}