public static class Day2
{
    public static void Run(string[] input)
    {
        Part1(input);
    }

    private static void Part1(string[] input)
    {
        long sum = 0;

        var numbers = input[0]
            .Split(",")
            .Select(line =>
            {
                var dash = line.IndexOf('-');
                var start = long.Parse(line[..dash]);
                var end = long.Parse(line[(dash+1)..]);
                return (start, end);
            });

        foreach (var (start, end) in numbers)
        {
            for (var i = start; i <= end; i++)
            {
                var num = i.ToString();
                var half = num.Length / 2;

                if (num[..half] == num[half..])
                {
                    sum += i;
                }
            }
        }

        Console.WriteLine(sum);
    }
}