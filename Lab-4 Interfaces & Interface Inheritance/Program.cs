using System;

public interface IVehicle
{
    string Model { get; }

    void Drive();
}

public interface IElectric
{
    int BatteryPercent { get; set; }

    void Charge();
}

// Combines both interfaces
public interface IElectricVehicle : IVehicle, IElectric
{
}

public class ElectricCar : IElectricVehicle
{
    // Model can only be assigned during object creation
    public string Model { get; init; }

    // Private backing field for battery
    private int _batteryPercent;

    // BatteryPercent property with 0-100 validation
    public int BatteryPercent
    {
        get
        {
            return _batteryPercent;
        }
        set
        {
            // Clamp value between 0 and 100
            _batteryPercent = Math.Clamp(value, 0, 100);
        }
    }

    // Driving reduces battery by 10%
    public void Drive()
    {
        BatteryPercent -= 10;

        // BatteryPercent setter prevents it going below 0
    }

    // Charging sets battery to 100%
    public void Charge()
    {
        BatteryPercent = 100;
    }
}

class Program
{
    static void Main()
    {
        // Create ElectricCar
        ElectricCar car = new ElectricCar
        {
            Model = "Tesla Model 3",
            BatteryPercent = 100
        };

        // Drive three times
        car.Drive();
        Console.WriteLine($"Battery after drive 1: {car.BatteryPercent}%");

        car.Drive();
        Console.WriteLine($"Battery after drive 2: {car.BatteryPercent}%");

        car.Drive();
        Console.WriteLine($"Battery after drive 3: {car.BatteryPercent}%");

        // Charge the car
        car.Charge();
        Console.WriteLine($"Battery after charge: {car.BatteryPercent}%");

        // Treat the object as an IVehicle
        IVehicle vehicle = car;

        Console.WriteLine(
            $"As IVehicle - Model: {vehicle.Model}");

        // Treat the same object as an IElectric
        IElectric electric = car;

        Console.WriteLine(
            $"As IElectric - BatteryPercent: {electric.BatteryPercent}%");
    }
}