using System;

class Node
{
    public int data;
    public Node left, right;

    public Node(int value)
    {
        data = value;
        left = right = null;
    }
}

class Solution
{
    static Node Insert(Node root, int data)
    {
        if (root == null)
            return new Node(data);

        if (data <= root.data)
            root.left = Insert(root.left, data);
        else
            root.right = Insert(root.right, data);

        return root;
    }

    static void preOrder(Node root)
    {
        if (root == null)
            return;

        Console.Write(root.data + " ");
        preOrder(root.left);
        preOrder(root.right);
    }

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        string[] values = Console.ReadLine().Split(' ');

        Node root = null;

        for (int i = 0; i < n; i++)
        {
            root = Insert(root, int.Parse(values[i]));
        }

        preOrder(root);
    }
}