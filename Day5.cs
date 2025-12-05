using System.Runtime.CompilerServices;

public static class Day5
{
    public static void Run(string[] input)
    {
        var split = input.IndexOf("");
        var ranges = input[0..split];
        var items = input[(split+1)..];

        var haw = new List<(long, long)>();

        foreach (var range in ranges)
        {
            var mid = range.IndexOf('-');
            var start = long.Parse(range[..mid]);
            var end = long.Parse(range[(mid+1)..]);

            haw.Add((start, end));
        }

        Part1(items, haw);
        Part2(haw);
    }

    private static void Part1(string[] items, List<(long, long)> haw)
    {
        var count = 0;

        foreach (var item in items)
        {
            var current = long.Parse(item);

            foreach (var yee in haw)
            {
                if (yee.Item1 < current && current < yee.Item2)
                {
                    count++;
                    break;
                }
            }
        }

        Console.WriteLine(count);
    }

    private static void Part2(List<(long,long)> haw)
    {
        while (ThereWasSomethingToMerge(haw));

        long count = 0;

        foreach (var yee in haw)
        {
            count += yee.Item2 - yee.Item1 + 1;
        }

        Console.WriteLine(count);
    }

    private static bool ThereWasSomethingToMerge(List<(long low, long high)> haw)
    {
        for (var i = 0; i < haw.Count; i++)
        {
            var yee = haw[i];

            for (var j = 0; j < haw.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                var yee2 = haw[j];

                if (IsOverlapping(yee, yee2))
                {
                    haw.Remove(yee);
                    haw.Remove(yee2);

                    var biggestRange = (Math.Min(yee.low, yee2.low), Math.Max(yee.high, yee2.high));

                    haw.Add(biggestRange);
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsOverlapping((long Low, long High) a, (long Low, long High) b) => 
        (a.Low <= b.High && a.Low >= b.Low) || 
        (a.High <= b.High && a.High >= b.Low);
}