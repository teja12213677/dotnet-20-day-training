using System;

class Node
{
    public int data;
    public Node left;
    public Node right;

    public Node(int data)
    {
        this.data = data;
        left = null;
        right = null;
    }
}

class Program
{
    static void PostOrder(Node root)
    {
        // Base condition
        if (root == null)
            return;

        // Visit left subtree
        PostOrder(root.left);

        // Visit right subtree
        PostOrder(root.right);

        // Visit root
        Console.Write(root.data + " ");
    }

    static void Main(string[] args)
    {
        // Creating the tree
        Node root = new Node(1);

        root.left = new Node(2);
        root.right = new Node(3);

        root.left.left = new Node(4);
        root.left.right = new Node(5);

        Console.WriteLine("Postorder Traversal:");

        PostOrder(root);

        Console.ReadLine();
    }
}