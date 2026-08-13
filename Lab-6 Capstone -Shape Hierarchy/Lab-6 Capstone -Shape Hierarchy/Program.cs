using System;
using System.Collections.Generic;
using System.Linq;

public enum ShapeKind
{
    Circle,
    Rectangle,
    Triangle
}

public abstract class Shape
{
    public ShapeKind Kind { get; protected set; }

    public abstract double Area();

    public abstract double Perimeter();

    public override string ToString()
    {
        return $"{Kind}: Area={Area():F2}, Perimeter={Perimeter():F2}";
    }
}

public class Circle : Shape
{
    public double Radius { get; }

    public Circle(double radius)
    {
        Kind = ShapeKind.Circle;
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height)
    {
        Kind = ShapeKind.Rectangle;
        Width = width;
        Height = height;
    }

    public override double Area()
    {
        return Width * Height;
    }

    public override double Perimeter()
    {
        return 2 * (Width + Height);
    }
}

public class Triangle : Shape
{
    public double A { get; }
    public double B { get; }
    public double C { get; }

    public Triangle(double a, double b, double c)
    {
        Kind = ShapeKind.Triangle;
        A = a;
        B = b;
        C = c;
    }

    public override double Area()
    {
   
        double s = (A + B + C) / 2;

        return Math.Sqrt(
            s * (s - A) * (s - B) * (s - C));
    }

    public override double Perimeter()
    {
        return A + B + C;
    }
}

public struct BoundingBox
{
    public double Width;
    public double Height;

    public BoundingBox(double w, double h)
    {
        Width = w;
        Height = h;
    }
    public static BoundingBox operator *(
        BoundingBox box,
        double factor)
    {
        return new BoundingBox(
            box.Width * factor,
            box.Height * factor);
    }

    public override string ToString()
    {
        return $"({Width:0.##}, {Height:0.##})";
    }
}

public static class ShapeMath
{
    public static double TotalArea(
        IEnumerable<Shape> shapes)
    {
        double total = 0;

        foreach (Shape shape in shapes)
        {
            total += shape.Area();
        }

        return total;
    }

    public static double TotalArea(
        IEnumerable<Shape> shapes,
        ShapeKind onlyKind)
    {
        double total = 0;

        foreach (Shape shape in shapes)
        {
            if (shape.Kind == onlyKind)
            {
                total += shape.Area();
            }
        }

        return total;
    }
}

public class Program
{
    public static void Main()
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Circle(3));
        shapes.Add(new Rectangle(4, 6));

        shapes.Add(new Triangle(3, 4, 5));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine(shape);
        }

        Console.WriteLine();


        double totalArea = ShapeMath.TotalArea(shapes);

        Console.WriteLine(
            $"Total area (all shapes): {totalArea:F2}");

        double circleArea =
            ShapeMath.TotalArea(
                shapes,
                ShapeKind.Circle);

        Console.WriteLine(
            $"Total area (circles only): {circleArea:F2}");

        Console.WriteLine();

        BoundingBox box = new BoundingBox(4, 3);

        BoundingBox scaledBox = box * 2;

        Console.WriteLine(
            $"Scaled bounding box {box} * 2 -> {scaledBox}");
    }
}