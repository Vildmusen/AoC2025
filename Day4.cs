public static class Day4
{
    public static void Run(string[] input)
    {
        var boxInfo = new Dictionary<(int, int), List<(int, int)>>();
     
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '@')
                {
                    var neighbours = CountNeighbours(i, j, input);
                    boxInfo.Add((i, j), neighbours);
                }
            }
        }

        Console.WriteLine(boxInfo.Count(b => b.Value.Count < 4));

        var removed = 0;
        var boxesToRemove = boxInfo.Count(b => b.Value.Count < 4);

        while (boxesToRemove > 0)
        {
            foreach (var key in boxInfo.Keys.ToList())
            {
                if (boxInfo[key].Count < 4)
                {
                    foreach (var list in boxInfo.Values)
                    {
                        if (list.Remove(key))
                        {
                            if (list.Count == 3)
                            {
                                boxesToRemove++;
                            }
                        }
                    }

                    boxInfo.Remove(key);
                    removed++;
                    boxesToRemove--;
                }
            }
        }

        Console.WriteLine(removed);
    }

    private static List<(int, int)> CountNeighbours(int x, int y, string[] lines)
    {
        var max = lines[0].Length;
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

                if (lines[i][j] == '@')
                {
                    list.Add((i, j));
                }
            }
        }

        return list;
    }
}