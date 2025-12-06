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

        foreach (var line in input[..^1])
        {
            var numbers = GetAllNumbers(line);

            for (int i = 0; i < numbers.Count; i++)
            {
                if (i == rows.Count)
                {
                    rows.Add([]);
                }

                rows[i].Add(numbers.ElementAt(i));
            }
        }

        var operators = input[^1]
            .Where(c => c != ' ');

        var sums = operators
            .Select((op, i) => 
                op == '*' ?
                    rows[i].Aggregate((a, b) => a * b) :
                    rows[i].Aggregate((a, b) => a + b));
        
        Console.WriteLine(sums.Sum());
    }

    private static List<long> GetAllNumbers(string line)
    {
        var numbers = new List<long>();
        var currentNumber = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == ' ' && currentNumber.Trim() != "")
            {
                numbers.Add(long.Parse(currentNumber));
                currentNumber = "";
            }
            else
            {
                currentNumber += line[i];
            }
        }

        numbers.Add(long.Parse(currentNumber));

        return numbers;
    }

    public static void Part2(string[] input)
    {
        var allSums = new List<long>();
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
                    break;
                }
                
                currentNum += current;
            }

            currentNums.Add(long.Parse(currentNum.Trim()));

            if (op != null)
            {
                if (op == '+')
                {
                    allSums.AddRange(currentNums);
                }
                else
                {
                    allSums.Add(currentNums.Aggregate((a, b) => a * b));
                }

                currentNums.Clear();
                op = null;
                i--; // because next column is completely blank
            }
        }

        Console.WriteLine(allSums.Sum());
    }
}