public static class Day8
{
    private static List<Node> _nodes = [];
    public static void Run(string[] input)
    {
        foreach (var node in input)
        {
            var parts = node.Split(',');
            _nodes.Add((
                new Node()
                {
                    X = long.Parse(parts[0]),
                    Y = long.Parse(parts[1]),
                    Z = long.Parse(parts[2]),
                }
            ));
        }

        Connect(1000);
    }

    public static void Connect(int iterations)
    {
        List<(double length, int x, int y)> calcCache = [];

        for (int i = 0; i < _nodes.Count; i++)
        {
            var a = _nodes[i];

            for (int j = i + 1; j < _nodes.Count; j++)
            {
                var b = _nodes[j];
                calcCache.Add((Distance(a, b), i, j));
            }
        }

        calcCache.Sort();
        var index = 0;
    
        while (index < iterations)
        {
            var (a, b) = AddSomeShit(index, calcCache);
            index++;
        }

        var res = TravelGraph(_nodes).Distinct().ToList();

        res.Sort((a, b) => a < b ? 1 : -1);
        var sum = res[..3].Aggregate((a, b) => a * b);
        Console.WriteLine(sum);

        while (true)
        {
            var (a, b) = AddSomeShit(index, calcCache);
            res = TravelGraph(_nodes);

            if (res.Count == 1)
            {
                var result = a.X * b.X;
                Console.WriteLine(result);
                break;
            }

            index++;
        }
    }

    public static (Node, Node) AddSomeShit(int index, List<(double length, int x, int y)> calcCache)
    {
        var (_, i, j) = calcCache[index];
        var a = _nodes[i];
        var b = _nodes[j]; 
        a.Connections.Add(b);
        b.Connections.Add(a);
        return (a, b);
    }

    public static List<int> TravelGraph(List<Node> bestNodes)
    {
        var visited = new HashSet<Node>();
        var sizes = new List<int>();

        foreach (var node in bestNodes)
        {
            if (!visited.Contains(node))
            {
                int size = TraverseMyShit(node, visited);
                sizes.Add(size);
            }
        }

        return sizes;
    }

    public static int TraverseMyShit(Node current, HashSet<Node> visited)
    {
        if (visited.Contains(current))
        {
            return 0;
        }

        var total = 1;
        visited.Add(current);

        foreach (var node in current.Connections)
        {
            total += TraverseMyShit(node, visited);
        }

        return total;
    }

    public static double Distance(Node a, Node b) =>
        Math.Sqrt(
            Math.Pow(a.X - b.X, 2) +
            Math.Pow(a.Y - b.Y, 2) +
            Math.Pow(a.Z - b.Z, 2)
        );
}

public class Node
{
    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }
    public List<Node> Connections = [];
    public string Print() => $"{X}.{Y}.{Z}";
}