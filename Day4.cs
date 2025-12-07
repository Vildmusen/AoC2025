public static class Day4
{
    private static (int, int)[] _neighbours = 
    [
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1), (1, 0), (1, 1),
    ];

    private static int _maxRows = 0;
    private static int _maxCols = 0;

    /// <summary>
    /// Omg Amazon Q is so handsome
    /// </summary>
    /// <param name="input"></param>
    public static void Run(string[] input)
    {
        _maxRows = input.Length;
        _maxCols = input[0].Length;
        var numeros = new int[_maxRows][];

        for (int i = 0; i < _maxRows; i++)
        {
            numeros[i] = new int[_maxCols];

            for (int j = 0; j < _maxCols; j++)
            {
                if (input[i][j] == '@')
                {
                    numeros[i][j] = CountNeighboursString(i, j, input);
                }
                else
                {
                    numeros[i][j] = 0;
                }
            }
        }

        var boxesToRemove = numeros
            .SelectMany(b => b)
            .Count(b => b < 4 && b != 0);

        Console.WriteLine(boxesToRemove);

        var removed = 0;
        var previousLapRemoved = int.MaxValue;

        while (previousLapRemoved != removed)
        {
            previousLapRemoved = removed;

            for (int i = 0; i < numeros.Length; i++)
            {
                for (int j = 0; j < numeros[i].Length; j++)
                {
                    if (numeros[i][j] != 0 && CountNeighbours(i, j, numeros) < 4)
                    {
                        removed += DoYaThing(i, j, numeros);
                    }
                }
            }
        }

        Console.WriteLine(removed);
    }

    private static int CountNeighboursString(int x, int y, string[] lines) =>
        _neighbours
            .Where(n => 
                InBounds(x + n.Item1, y + n.Item2) && 
                lines[x + n.Item1][y + n.Item2] == '@')
            .Count();

    private static int CountNeighbours(int x, int y, int[][] boxes) =>
        _neighbours
            .Where(n => 
                InBounds(x + n.Item1, y + n.Item2) 
                && boxes[x + n.Item1][y + n.Item2] > 0)
            .Count();

    private static int DoYaThing(int x, int y, int[][] boxes)
    {
        var goneCount = 1;

        var toModify = _neighbours
            .Where(n => 
                InBounds(x + n.Item1, y + n.Item2) && 
                boxes[x + n.Item1][y + n.Item2] != 0)
            .Select(n => (x + n.Item1, y + n.Item2))
            .ToList();

        foreach (var (row, col) in toModify)
        {
            boxes[row][col] -= 1;

            if (boxes[row][col] == 0)
            {
                goneCount++;
            }
        }        

        boxes[x][y] = 0;
        return goneCount;
    }

    private static bool InBounds(int x, int y) => 
        x >= 0 && 
        x < _maxRows && 
        y >= 0 && 
        y < _maxCols;
}