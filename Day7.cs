public static class Day7
{
    public static void Run(string[] input)
    {
        Part1And2(input);
    }

    public static void Part1And2(string[] input)
    {
        var start = input[0].IndexOf('S');
        List<int> beamos = [start];
        Dictionary<int, long> ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex = new() { {start, 1L} };
        var count = 0;

        foreach (var line in input[1..])
        {
            List<int> splitters = [];

            foreach (var beam in beamos)
            {
                if (line[beam] == '^')
                {
                    count++;

                    var left = beam - 1;
                    var right = beam + 1;

                    splitters.AddRange([left, right]);

                    var toAdd = ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex[beam];
                    AddOrAddTo(ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex, left, toAdd);
                    AddOrAddTo(ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex, right, toAdd);
                    ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex.Remove(beam);
                }
                else
                {
                    splitters.Add(beam);
                }
            }

            beamos.Clear();
            beamos.AddRange(splitters.Distinct());
            splitters.Clear();
        }

        Console.WriteLine(count);
        Console.WriteLine(ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex.Values.Sum());
    }

    private static void AddOrAddTo(Dictionary<int, long> ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex, int key, long toAdd)
    {
        if (ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex.ContainsKey(key))
        {
            ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex[key] += toAdd;
        }
        else
        {
            ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndex.Add(key, toAdd);
        }
    }
}