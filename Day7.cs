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
        Dictionary<int, long> ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndexWeShouldStoreTheirTotalValueInHere = new() { {start, 1L} };
        var count = 0;

        foreach (var line in input[1..])
        {
            HashSet<int> splitters = [];

            foreach (var beam in beamos)
            {
                if (line[beam] == '^')
                {
                    count++;

                    IMAFIRINGMAHLASER(
                        beam,
                        splitters, 
                        ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndexWeShouldStoreTheirTotalValueInHere);
                }
                else
                {
                    splitters.Add(beam);
                }
            }

            beamos.Clear();
            beamos.AddRange(splitters);
            splitters.Clear();
        }

        Console.WriteLine(count);
        Console.WriteLine(ifTheBeamsTookDifferentPathsAndEndedUpOnTheSameIndexWeShouldStoreTheirTotalValueInHere.Values.Sum());
    }

    private static void IMAFIRINGMAHLASER(
        int beam,
        HashSet<int> splitters, 
        Dictionary<int, long> someDictionary)
    {
        var left = beam - 1;
        var right = beam + 1;

        splitters.Add(left);
        splitters.Add(right);

        var toAdd = someDictionary[beam];

        if (!someDictionary.TryAdd(left, toAdd))
        {
            someDictionary[left] += toAdd;
        }
        if (!someDictionary.TryAdd(right, toAdd))
        {
            someDictionary[right] += toAdd;
        }

        someDictionary.Remove(beam);
    }
}