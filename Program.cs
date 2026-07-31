using System;
using System.Collections.Generic;

class SocialNetwork
{
    private int vertices;
    private List<int>[] graph;

    public SocialNetwork(int v)
    {
        vertices = v;
        graph = new List<int>[v];

        for (int i = 0; i < v; i++)
        {
            graph[i] = new List<int>();
        }
    }

    public void AddFriendship(int u, int v)
    {
        graph[u].Add(v);
        graph[v].Add(u);
    }


    public void FriendsOfUser(int user)
    {
        Console.WriteLine("Friends of User " + user + ":");

        foreach (int friend in graph[user])
        {
            Console.Write(friend + " ");
        }
        Console.WriteLine();
    }

    public bool AreConnected(int start, int end)
    {
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == end)
                return true;

            foreach (int neighbor in graph[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }


    public void ShortestPath(int start, int end)
    {
        bool[] visited = new bool[vertices];
        int[] parent = new int[vertices];

        for (int i = 0; i < vertices; i++)
            parent[i] = -1;

        Queue<int> queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int neighbor in graph[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        Console.Write("Shortest Path: ");

        Stack<int> path = new Stack<int>();

        int temp = end;

        while (temp != -1)
        {
            path.Push(temp);
            temp = parent[temp];
        }

        while (path.Count > 0)
        {
            Console.Write(path.Pop());

            if (path.Count > 0)
                Console.Write(" -> ");
        }

        Console.WriteLine();
    }


    public void UsersAtDistanceTwo(int source)
    {
        int[] distance = new int[vertices];

        for (int i = 0; i < vertices; i++)
            distance[i] = -1;

        Queue<int> queue = new Queue<int>();

        distance[source] = 0;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int neighbor in graph[current])
            {
                if (distance[neighbor] == -1)
                {
                    distance[neighbor] = distance[current] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        Console.WriteLine("Users at distance 2 from User " + source + ":");

        for (int i = 0; i < vertices; i++)
        {
            if (distance[i] == 2)
                Console.Write(i + " ");
        }

        Console.WriteLine();
    }


    public bool HasCycle()
    {
        bool[] visited = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            if (!visited[i])
            {
                if (CycleDFS(i, visited, -1))
                    return true;
            }
        }

        return false;
    }

    private bool CycleDFS(int current, bool[] visited, int parent)
    {
        visited[current] = true;

        foreach (int neighbor in graph[current])
        {
            if (!visited[neighbor])
            {
                if (CycleDFS(neighbor, visited, current))
                    return true;
            }
            else if (neighbor != parent)
            {
                return true;
            }
        }

        return false;
    }

  
    public void ConnectedComponents()
    {
        bool[] visited = new bool[vertices];

        Console.WriteLine("Connected Components:");

        for (int i = 0; i < vertices; i++)
        {
            if (!visited[i])
            {
                DFS(i, visited);
                Console.WriteLine();
            }
        }
    }

    private void DFS(int node, bool[] visited)
    {
        visited[node] = true;
        Console.Write(node + " ");

        foreach (int neighbor in graph[node])
        {
            if (!visited[neighbor])
                DFS(neighbor, visited);
        }
    }
}

class Program
{
    static void Main()
    {
        SocialNetwork sn = new SocialNetwork(6);

        sn.AddFriendship(0, 1);
        sn.AddFriendship(0, 2);
        sn.AddFriendship(1, 3);
        sn.AddFriendship(2, 3);
        sn.AddFriendship(2, 4);
        sn.AddFriendship(3, 5);
        sn.AddFriendship(4, 5);

        sn.FriendsOfUser(2);

 
        Console.WriteLine("User 0 and User 5 Connected: " + sn.AreConnected(0, 5));

        sn.ShortestPath(0, 5);

        sn.UsersAtDistanceTwo(1);

        if (sn.HasCycle())
            Console.WriteLine("Network contains a cycle.");
        else
            Console.WriteLine("Network does not contain a cycle.");

        sn.ConnectedComponents();
    }
}