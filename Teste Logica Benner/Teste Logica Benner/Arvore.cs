public class Arvore
{
    private Dictionary<int, HashSet<int>> graph;
    private int size;

    public Arvore(int n)
    {
        if (n <= 0)
            throw new ArgumentException("Number of elements must be positive.");

        size = n;
        graph = new Dictionary<int, HashSet<int>>();
        for (int i = 1; i <= n; i++)
            graph[i] = new HashSet<int>();
    }

    public void Connect(int a, int b)
    {
        ValidateNodes(a, b);
        graph[a].Add(b);
        graph[b].Add(a);
    }

    public void Disconnect(int a, int b)
    {
        ValidateNodes(a, b);
        graph[a].Remove(b);
        graph[b].Remove(a);
    }

    public bool Query(int a, int b)
    {
        ValidateNodes(a, b);
        return DFS(a, b, new HashSet<int>());
    }

    public int LevelConnection(int a, int b)
    {
        ValidateNodes(a, b);
        return BFSLevel(a, b);
    }

    private bool DFS(int current, int target, HashSet<int> visited)
    {
        if (current == target) return true;
        visited.Add(current);
        foreach (int neighbor in graph[current])
        {
            if (!visited.Contains(neighbor))
                if (DFS(neighbor, target, visited))
                    return true;
        }
        return false;
    }

    private int BFSLevel(int start, int target)
    {
        if (start == target) return 1;
        var visited = new HashSet<int>();
        var queue = new Queue<(int node, int level)>();
        queue.Enqueue((start, 0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (node, level) = queue.Dequeue();
            foreach (int neighbor in graph[node])
            {
                if (!visited.Contains(neighbor))
                {
                    if (neighbor == target)
                        return level + 1;
                    queue.Enqueue((neighbor, level + 1));
                    visited.Add(neighbor);
                }
            }
        }

        return 0; // Not connected
    }

    private void ValidateNodes(int a, int b)
    {
        if (a < 1 || b < 1 || a > size || b > size)
            throw new ArgumentException("Invalid node index.");
    }
}
