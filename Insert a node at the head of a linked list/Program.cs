using System;

class Node
{
    public int Data;
    public Node Next;

    public Node(int data)
    {
        Data = data;
        Next = null;
    }
}

class LinkedList
{
    Node head;

    public void InsertAtHead(int data)
    {
        Node newNode = new Node(data);
        newNode.Next = head;

        head = newNode;
    }

    public void Display()
    {
        Node current = head;

        while (current != null)
        {
            Console.Write(current.Data + " -> ");
            current = current.Next;
        }

        Console.WriteLine("NULL");
    }
}

class Program
{
    static void Main()
    {
        LinkedList list = new LinkedList();

        list.InsertAtHead(30);
        list.InsertAtHead(20);
        list.InsertAtHead(10);

        list.Display();
    }
}