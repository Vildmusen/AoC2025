public static class Day6
{
    public static void Run(string[] input)
    {
        var rows = new List<List<long>>();

        foreach (var line in input.SkipLast(1))
        {
            var numbers = line
                .Replace("    ", "   ")
                .Replace("   ", "  ")
                .Replace("  ", " ")
                .Split(" ")
                .Where(i => i != "")
                .Select(long.Parse);

            for (int i = 0; i < numbers.Count(); i++)
            {
                if (i == rows.Count)
                {
                    rows.Add([]);
                }

                rows[i].Add(numbers.ElementAt(i));
            }
        }

        var operators = input
            .Last()
            .Replace("    ", "   ")
            .Replace("   ", "  ")
            .Replace("  ", " ")
            .Replace(" ", "");

        var sums = operators
            .Select((op, i) => 
                op == '*' ?
                    rows[i].Aggregate((a, b) => a * b) :
                    rows[i].Aggregate((a, b) => a + b));
        
        Console.WriteLine(sums.Sum());
    }
}