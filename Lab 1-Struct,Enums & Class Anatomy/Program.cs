using System;

public struct RgbColor
{
    public byte R, G, B;

    public RgbColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public override string ToString()
    {
        return $"#{R:X2}{G:X2}{B:X2}";
    }
}

public enum NamedColor
{
    Red,
    Green,
    Blue,
    White,
    Black
}

public class Program
{

    public static RgbColor FromNamed(NamedColor name)
    {
        switch (name)
        {
            case NamedColor.Red:
                return new RgbColor(255, 0, 0);

            case NamedColor.Green:
                return new RgbColor(0, 255, 0);

            case NamedColor.Blue:
                return new RgbColor(0, 0, 255);

            case NamedColor.White:
                return new RgbColor(255, 255, 255);

            case NamedColor.Black:
                return new RgbColor(0, 0, 0);

            default:
                return new RgbColor(0, 0, 0);
        }
    }

    public static void Main()
    {

        Console.WriteLine("-- struct copy --");

        RgbColor a = FromNamed(NamedColor.Red);

        RgbColor b = a;

        b.R = 1;

        Console.WriteLine($"a = {a}");
        Console.WriteLine($"b = {b}");

        Console.WriteLine();
        Console.WriteLine("-- class/reference copy --");

        Pixel p1 = new Pixel();
        p1.Color = FromNamed(NamedColor.Green);

        Pixel p2 = p1;

        p2.Color = new RgbColor(0, 255, 0);

        Console.WriteLine($"p1.Color = {p1.Color}");
        Console.WriteLine($"p2.Color = {p2.Color}");
    }
}

public class Pixel
{
    public RgbColor Color;
}