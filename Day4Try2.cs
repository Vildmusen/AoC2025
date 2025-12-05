public static class Day4Try2
{
    public static void Run(string[] input)
    {
        var boxes = input
            .Select((row, rowIndex) => row
                .Select((c, colIndex) => c == '@' ? CountNeighbours(rowIndex, colIndex, input) : [])
                .ToArray())
            .ToArray();

        while (YeetAndDelete(boxes!));
    }

    private static List<(int, int)> CountNeighbours(int x, int y, string[] boxes)
    {
        var max = boxes[0].Length;
        List<(int, int)> list = [];

        for (int i = x - 1; i < x + 2; i++)
        {
            if (i < 0 || i >= max)
            {
                continue;
            }

            for (int j = y - 1; j < y + 2; j++)
            {
                if (j < 0 || j >= max || (x == i && y == j))
                {
                    continue;
                }

                if (boxes[i][j] == '@')
                {
                    list.Add((i, j));
                }
            }
        }

        return list;
    }

    private static bool YeetAndDelete(List<(int, int)>[][] boxes)
    {
        var weDidIt = false;

        for (var i = 0; i < boxes.Length; i++)
        {
            for (var j = 0; j < boxes[0].Length; j++)
            {
                if (boxes[i][j].Count > 0 && boxes[i][j].Count < 4)
                {
                    var neighbours = boxes[i][j];
                    BreadthAndDestroy(neighbours, boxes, [(i, j)]);
                    weDidIt = true;
                }
            }
        }

        return weDidIt;
    }

    private static void BreadthAndDestroy(List<(int, int)> current, List<(int, int)>[][] all, List<(int, int)> visited)
    {
        if (current.Count > 3)
        {
            return;
        }

        while (current.Count > 0)
        {
            try
            {
                var box = current.First(b => !visited.Contains(b));
                visited.Add(box);
                current.RemoveAt(0);
                BreadthAndDestroy(all[box.Item1][box.Item2], all, visited);
            } 
            catch
            {
                return;
            }            
        }
    }
}