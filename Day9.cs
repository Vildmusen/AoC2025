public static class Day9
{
    public static void Run(string[] input)
    {
        var splitted = input
            .Select(i => i.Split(','))
            .Select(parts => (long.Parse(parts[0]), long.Parse(parts[1])))
            .ToList();

        Part1(splitted);
        Part2(splitted);
    }

    public static void Part1(List<(long, long)> splitted)
    {
        List<long> areas = [];

        for (int i = 0; i < splitted.Count; i++)
        {
            for (int j = i + 1; j < splitted.Count; j++)
            {
                var (x1, y1) = splitted[i];
                var (x2, y2) = splitted[j];

                var dx = Math.Abs(x2 - x1) + 1;
                var dy = Math.Abs(y2 - y1) + 1;

                areas.Add(dx * dy);
            }
        }

        Console.WriteLine(areas.Max());
    }

    public static void Part2(List<(long, long)> splitted)
    {
        var edges = new List<Edge>();

        // construct edges
        for (int i = 0; i < splitted.Count; i++)
        {
            var current = splitted[i];
            var previous = i == 0 ?
                splitted[^1] :
                splitted[i - 1];

            edges.Add(new(current.Item1, current.Item2, previous.Item1, previous.Item2));
        }

        List<long> areas = [];

        // check each and every point of every square :)
        for (int i = 0; i < splitted.Count; i++)
        {
            for (int j = i + 1; j < splitted.Count; j++)
            {
                var (x1, y1) = splitted[i];
                var (x2, y2) = splitted[j];

                var minX = Math.Min(x1, x2);
                var maxX = Math.Max(x1, x2);
                var minY = Math.Min(y1, y2);
                var maxY = Math.Max(y1, y2);

                if (!AllPointsInsideShape((minX, minY), (maxX, maxY), edges))
                {
                    continue;
                }

                var dx = Math.Abs(x2 - x1) + 1;
                var dy = Math.Abs(y2 - y1) + 1;
                
                areas.Add(dx * dy);
            }
        }

        Console.WriteLine(areas.Max());
    }

    public static bool AllPointsInsideShape((long X, long Y) start, (long X, long Y) end, List<Edge> edges)
    {
        // skip first and last rows of points, since they are guarenteed to be on an edge of the shape
        for (long x = start.X + 1; x <= end.X - 1; x++)
        {
            for (long y = start.Y + 1; y <= end.Y - 1; y++)
            {
                if (!IsInShape((x, y), edges))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool IsInShape((long X, long Y) dot, List<Edge> edges)
    {
        int crossings = 0;

        foreach (var e in edges)
        {
            if (e.X1 == e.X2 && e.X1 >= dot.X)
            {
                long y_min = Math.Min(e.Y1, e.Y2);
                long y_max = Math.Max(e.Y1, e.Y2);
                
                if (dot.Y > y_min && dot.Y <= y_max)
                {
                    crossings++;
                }
            }
        }

        return crossings % 2 == 1;
    }
}

public record Edge(long X1, long Y1, long X2, long Y2);