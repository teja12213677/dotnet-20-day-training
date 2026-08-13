using System;

public class LibraryBook
{

    private string _isbn;

    public string Title;

    protected string ShelfLocation = "Unassigned";

    internal int CopiesAvailable;

    public static int TotalBooksCreated;

    public LibraryBook(string title, string isbn)
    {
        Title = title;
        _isbn = isbn;

        CopiesAvailable = 1;

        TotalBooksCreated++;
    }

    protected internal void Relocate(string newLocation)
    {
        ShelfLocation = newLocation;
    }

    private protected void AdjustCopies(int delta)
    {
        CopiesAvailable += delta;
    }
}

public class ReferenceBook : LibraryBook
{
    public ReferenceBook(string title, string isbn)
        : base(title, isbn)
    {
    }

    public void PrintLocation()
    {
        Console.WriteLine(
            $"Initial shelf location: \"{ShelfLocation}\"");
        Relocate("Reference Section");

        Console.WriteLine(
            $"ReferenceBook shelf location after Relocate: \"{ShelfLocation}\"");
        AdjustCopies(2);

        Console.WriteLine(
            $"Copies available after AdjustCopies(+2): {CopiesAvailable}");
    }
}

public class Program
{
    public static void Main()
    {
   
        LibraryBook book1 = new LibraryBook(
            "C# Basics",
            "ISBN001");

        Console.WriteLine(
            $"Book 1 created. Total books so far: {LibraryBook.TotalBooksCreated}");

        LibraryBook book2 = new LibraryBook(
            "Object Oriented Programming",
            "ISBN002");

        Console.WriteLine(
            $"Book 2 created. Total books so far: {LibraryBook.TotalBooksCreated}");

        LibraryBook book3 = new LibraryBook(
            "Data Structures",
            "ISBN003");

        Console.WriteLine(
            $"Book 3 created. Total books so far: {LibraryBook.TotalBooksCreated}");

        Console.WriteLine();
        ReferenceBook referenceBook = new ReferenceBook(
            "C# Reference",
            "ISBN004");

        referenceBook.PrintLocation();
    }
}