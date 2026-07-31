using System;
using System.Collections.Generic;

class CourseGraph
{
    private int vertices;
    private List<int>[] graph;

    public CourseGraph(int v)
    {
        vertices = v;
        graph = new List<int>[v];

        for (int i = 0; i < v; i++)
        {
            graph[i] = new List<int>();
        }
    }

    public void AddEdge(int prerequisite, int course)
    {
        graph[prerequisite].Add(course);
    }

    public void FindPrerequisites(int targetCourse)
    {
        Console.WriteLine("All prerequisites for Course " + targetCourse + ":");

        bool[] visited = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            if (HasPath(i, targetCourse, visited))
            {
                Console.Write(i + " ");
            }
            Array.Fill(visited, false);
        }
        Console.WriteLine();
    }

    private bool HasPath(int source, int target, bool[] visited)
    {
        if (source == target)
            return false;

        visited[source] = true;

        foreach (int neighbor in graph[source])
        {
            if (neighbor == target)
                return true;

            if (!visited[neighbor] && HasPath(neighbor, target, visited))
                return true;
        }

        return false;
    }

    public void DirectPrerequisites(int course)
    {
        Console.WriteLine("Direct prerequisites of Course " + course + ":");

        for (int i = 0; i < vertices; i++)
        {
            if (graph[i].Contains(course))
                Console.Write(i + " ");
        }
        Console.WriteLine();
    }

    public bool HasCycle()
    {
        bool[] visited = new bool[vertices];
        bool[] recursion = new bool[vertices];

        for (int i = 0; i < vertices; i++)
        {
            if (CycleDFS(i, visited, recursion))
                return true;
        }

        return false;
    }

    private bool CycleDFS(int node, bool[] visited, bool[] recursion)
    {
        if (recursion[node])
            return true;

        if (visited[node])
            return false;

        visited[node] = true;
        recursion[node] = true;

        foreach (int neighbor in graph[node])
        {
            if (CycleDFS(neighbor, visited, recursion))
                return true;
        }

        recursion[node] = false;
        return false;
    }

    public void TopologicalSort()
    {
        int[] indegree = new int[vertices];

        for (int i = 0; i < vertices; i++)
        {
            foreach (int neighbor in graph[i])
            {
                indegree[neighbor]++;
            }
        }

        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < vertices; i++)
        {
            if (indegree[i] == 0)
                queue.Enqueue(i);
        }

        Console.WriteLine("Topological Order:");

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            Console.Write(current + " ");

            foreach (int neighbor in graph[current])
            {
                indegree[neighbor]--;

                if (indegree[neighbor] == 0)
                    queue.Enqueue(neighbor);
            }
        }

        Console.WriteLine();
    }

    public void NoPrerequisiteCourses()
    {
        int[] indegree = new int[vertices];

        for (int i = 0; i < vertices; i++)
        {
            foreach (int neighbor in graph[i])
                indegree[neighbor]++;
        }

        Console.WriteLine("Courses with no prerequisites:");

        for (int i = 0; i < vertices; i++)
        {
            if (indegree[i] == 0)
                Console.Write(i + " ");
        }

        Console.WriteLine();
    }

    public void CountDependents(int course)
    {
        Console.WriteLine("Courses directly depending on Course " + course + ": " + graph[course].Count);
    }
}

class Program
{
    static void Main()
    {
        CourseGraph cg = new CourseGraph(6);

        cg.AddEdge(0, 1);
        cg.AddEdge(0, 2);
        cg.AddEdge(1, 3);
        cg.AddEdge(2, 3);
        cg.AddEdge(2, 4);
        cg.AddEdge(3, 5);
        cg.AddEdge(4, 5);

        cg.FindPrerequisites(5);

        cg.DirectPrerequisites(3);

        if (cg.HasCycle())
        {
            Console.WriteLine("Graph contains a cycle.");
        }
        else
        {
            Console.WriteLine("Graph does not contain a cycle.");
            cg.TopologicalSort();
        }

        cg.NoPrerequisiteCourses();

        cg.CountDependents(2);
    }
}