public static class Day3
{
    public static void Run(string[] input)
    {
        Part1(input);
        Part2(input);
    }

    private static void Part1(string[] input)
    {
        FindTheBestNumberWithNDigits(input, 2);
    }

    private static void Part2(string[] input)
    {
        FindTheBestNumberWithNDigits(input, 12);
    }

    private static void FindTheBestNumberWithNDigits(string[] input, int numberLength)
    {
        var sum = 0L; 
        var lineLength = input[0].Length;

        foreach (var line in input)
        {
            var bestNums = "";
            var margin = lineLength - numberLength; 
            var current = 0;
            var usedMargin = 0;
            
            while (bestNums.Length < numberLength)
            {
                var (best, foundAt) = GetBest(line.Skip(current).Take(margin + 1));
                bestNums += best;

                current += foundAt + 1;
                usedMargin += foundAt;
                margin = lineLength - (numberLength + usedMargin);

                if (usedMargin == lineLength - numberLength)
                {
                    bestNums += line[current..];
                    break;
                }
            }

            sum += long.Parse(bestNums);
        }

        Console.WriteLine(sum);
    }

    private static (char, int) GetBest(IEnumerable<char> list)
    {        
        var indexOfBest = 0;
        var best = 0;
        var currentIndex = 0;

        foreach (var num in list)
        {
            if (num == '9')
            {
                return (num, currentIndex);
            }

            if (num > best)
            {
                best = num;
                indexOfBest = currentIndex;
            }

            currentIndex++;
        }

        return ((char)best, indexOfBest);
    }
}