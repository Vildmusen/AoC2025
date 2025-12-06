using System.Runtime.InteropServices;

public static class Day6
{
    public static void Run(string[] input)
    {
        Part1(input);
        Part2(input);
    }

    public static void Part1(string[] input)
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

    public static void Part2(string[] input)
    {
        long sum = 0;
        var currentNums = new List<long>();

        for (int i = input[0].Length - 1; i >= 0; i--)
        {
            var currentNum = "";
            char? op = null;

            for (int j = 0; j < input.Length; j++)
            {
                var current = input[j][i];

                if (current == ' ')
                {
                    continue;
                }

                if (current == '+' || current == '*')
                {
                    op = current;
                }
                else
                {
                    currentNum += current;
                }
            }

            if (currentNum == "")
            {
                continue;
            }

            currentNums.Add(long.Parse(currentNum.Trim()));

            if (op != null)
            {
                sum += op == '+' ?
                    currentNums.Aggregate((a, b) => a + b) :
                    currentNums.Aggregate((a, b) => a * b);

                currentNums.Clear();
                op = null;
            }
        }

        Console.WriteLine(sum);
    }
}