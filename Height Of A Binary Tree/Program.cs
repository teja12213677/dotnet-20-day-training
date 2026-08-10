using System;

class Node
{
    public int Data;
    public Node Left;
    public Node Right;

    public Node(int data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}

class BinaryTree
{
    public Node Root;

    // Find Height of Tree
    public int Height(Node root)
    {
        // Empty tree
        if (root == null)
            return -1;

        int leftHeight = Height(root.Left);
        int rightHeight = Height(root.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }
}

class Program
{
    static void Main()
    {
        BinaryTree tree = new BinaryTree();

        // Creating the tree
        tree.Root = new Node(3);

        tree.Root.Left = new Node(2);
        tree.Root.Right = new Node(5);

        tree.Root.Left.Left = new Node(1);

        tree.Root.Right.Left = new Node(4);
        tree.Root.Right.Right = new Node(6);

        tree.Root.Right.Right.Right = new Node(7);

        Console.WriteLine("Height of Binary Tree = " + tree.Height(tree.Root));
    }
}